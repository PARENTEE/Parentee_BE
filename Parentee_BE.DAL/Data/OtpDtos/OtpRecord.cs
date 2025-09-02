namespace Parentee_BE.DAL.Data.OtpDtos;

public class OtpRecord
{
    public string PhoneE164 { get; set; } = default!;
    public string OtpHash { get; set; } = default!;
    public DateTimeOffset ExpiresAt { get; set; }
    public int Attempts { get; set; }
    public int ResendCount { get; set; }
    public DateTimeOffset? NextResendAt { get; set; }
}