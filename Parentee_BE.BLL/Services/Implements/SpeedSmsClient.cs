using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.Sms;
using Parentee_BE.DAL.Data.ResponseDTO.Sms;
using Parentee_BE.DAL.Data.SmsDTO;

namespace Parentee_BE.BLL.Services.Implements;

public class SpeedSmsClient : ISmsSender
{
     private readonly HttpClient _http;
    private readonly SpeedSmsOptions _opts;

    public SpeedSmsClient(HttpClient http, IOptions<SpeedSmsOptions> opts)
    {
        _http = http;
        _opts = opts.Value;

        // Basic auth: base64("token:x")
        var token = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_opts.ApiToken}:x"));
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
        _http.BaseAddress = new Uri(_opts.BaseUrl.TrimEnd('/') + "/");
    }

    public async Task<SpeedSmsSendResult> SendAsync(IEnumerable<string> to, string content, int? smsType = null, string? sender = null, CancellationToken ct = default)
    {
        var payload = new
        {
            to = to.ToArray(),          // support nhiều số (tối đa 100) :contentReference[oaicite:8]{index=8}
            content,                    // Unicode OK; 160 ascii / 70 unicode; tối đa 3 SMS :contentReference[oaicite:9]{index=9}
            sms_type = smsType ?? _opts.DefaultSmsType,
            sender = sender ?? _opts.Sender // bắt buộc nếu sms_type=3 hoặc 5 :contentReference[oaicite:10]{index=10}
        };

        using var req = new HttpRequestMessage(HttpMethod.Post, "sms/send")
        {
            Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };

        using var res = await _http.SendAsync(req, ct);
        var json = await res.Content.ReadAsStringAsync(ct);

        var dto = JsonSerializer.Deserialize<SpeedSmsSendResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                  ?? throw new InvalidOperationException("Empty response from SpeedSMS");

        if (!res.IsSuccessStatusCode || dto.Status != "success")
        {
            throw new InvalidOperationException($"SpeedSMS error {dto.Code}: {dto.Message}");
        }

        return new SpeedSmsSendResult(dto.Data!.TranId, dto.Data.TotalSMS, dto.Data.TotalPrice, dto.Data.InvalidPhone ?? new List<string>());
    }

    public async Task<IReadOnlyList<SpeedSmsStatusItem>> GetStatusAsync(long tranId, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"sms/status/{tranId}");
        using var res = await _http.SendAsync(req, ct);
        var json = await res.Content.ReadAsStringAsync(ct);

        var dto = JsonSerializer.Deserialize<SpeedSmsStatusResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                  ?? throw new InvalidOperationException("Empty response from SpeedSMS");

        if (!res.IsSuccessStatusCode || dto.Status != "success")
            throw new InvalidOperationException($"SpeedSMS error {dto.Code}: {dto.Message}");

        return dto.Data ?? new List<SpeedSmsStatusItem>();
    }
}