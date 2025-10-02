namespace Parentee_BE.DAL.Data.RequestDTO.Sms;

public class SendSmsRequest
{
    public string To { get; set; } = string.Empty;
    public string Content { get; set; } = default!;
    // public int? SmsType { get; set; } // 2/3/4/5/6
    public string? Sender { get; set; } // brandname if smsType=3
}