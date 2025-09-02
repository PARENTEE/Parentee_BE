namespace Parentee_BE.DAL.Data.OtpDtos.Options;

public class TwilioOptions
{
    public string AccountSid { get; set; } = default!;
    public string AuthToken  { get; set; } = default!;
    public string FromNumber { get; set; } = default!;
}