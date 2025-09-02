using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Parentee_BE.BLL.Services.Interfaces.OtpInterface;
using Parentee_BE.DAL.Data.OtpDtos;
using Parentee_BE.DAL.Data.OtpDtos.Options;
using PhoneNumbers;

namespace Parentee_BE.BLL.Services.Implements.OtpImplement;

public class OtpService
{
    private readonly IOtpStore _store;
    private readonly ISmsSender _sms;
    private readonly OtpOptions _opt;

    public OtpService(IOtpStore store, ISmsSender sms, IOptions<OtpOptions> opt)
    {
        _store = store;
        _sms = sms;
        _opt = opt.Value;
    }

    private static string NormalizeE164(string raw, string defaultRegion = "VN")
    {
        var util = PhoneNumberUtil.GetInstance();
        var parsed = util.Parse(raw, defaultRegion); // ví dụ VN
        return util.Format(parsed, PhoneNumberFormat.E164); // +84...
    }

    private static string RandomOtp6()
    {
        // Crypto-safe 6 digits
        var bytes = RandomNumberGenerator.GetBytes(4);
        var val = BitConverter.ToUInt32(bytes, 0) % 900000 + 100000;
        return val.ToString();
    }

    public async Task<SendOtpResponse> SendOtpAsync(string rawPhone, string? transactionId = null)
    {
        var phone = NormalizeE164(rawPhone);
        var key = string.IsNullOrWhiteSpace(transactionId) ? $"otp:{phone}" : transactionId;

        var now = DateTimeOffset.UtcNow;
        var existing = await _store.GetAsync(key);
        if (existing?.NextResendAt is not null && now < existing.NextResendAt)
        {
            var wait = (long)(existing.NextResendAt.Value - now).TotalSeconds;
            return new SendOtpResponse(key, Math.Max(wait, 1));
        }

        var otp = RandomOtp6();
        var hash = BCrypt.Net.BCrypt.HashPassword(otp); // lưu HASH, không lưu plain

        var rec = new OtpRecord
        {
            PhoneE164 = phone,
            OtpHash = hash,
            ExpiresAt = now.AddMinutes(_opt.OtpTtlMinutes),
            Attempts = 0,
            ResendCount = (existing?.ResendCount ?? 0) + 1,
            NextResendAt = now.AddSeconds(_opt.CooldownSeconds)
        };

        await _store.PutAsync(key, rec, TimeSpan.FromMinutes(_opt.OtpTtlMinutes + 5));

        // Gửi SMS
        await _sms.SendAsync(phone, $"Your verification code is: {otp} (valid {_opt.OtpTtlMinutes} minutes)");

        return new SendOtpResponse(key, _opt.CooldownSeconds);
    }

    public async Task<(bool ok, string reason)> VerifyAsync(string transactionId, string otp)
    {
        var rec = await _store.GetAsync(transactionId);
        if (rec is null) return (false, "not_found_or_expired");

        var now = DateTimeOffset.UtcNow;
        if (now > rec.ExpiresAt) return (false, "expired");
        if (rec.Attempts >= _opt.MaxAttempts) return (false, "too_many_attempts");

        var ok = BCrypt.Net.BCrypt.Verify(otp, rec.OtpHash);

        // cập nhật attempts (kể cả đúng hay sai) để tránh brute force
        rec.Attempts += 1;

        // Giữ TTL còn lại (đừng reset ExpiresAt), chỉ cập nhật attempts
        var remaining = rec.ExpiresAt - now;
        if (remaining < TimeSpan.Zero) remaining = TimeSpan.FromSeconds(1);

        if (ok)
        {
            // one-time → xóa khỏi store
            await _store.DeleteAsync(transactionId);
            return (true, "ok");
        }

        await _store.PutAsync(transactionId, rec, remaining);
        return (false, "invalid");
    }
}