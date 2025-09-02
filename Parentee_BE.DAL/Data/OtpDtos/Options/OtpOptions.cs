namespace Parentee_BE.DAL.Data.OtpDtos.Options;

public class OtpOptions
{
    public int OtpTtlMinutes { get; set; } = 5;
    public int CooldownSeconds { get; set; } = 45;
    public int MaxAttempts { get; set; } = 5;
}