namespace Parentee_BE.DAL.Data.SmsDTO;

public class SpeedSmsOptions
{
    public string BaseUrl { get; set; } = "https://v1.tingting.im/api";
    public string ApiToken { get; set; } = default!;
    public int DefaultSmsType { get; set; } = 2; // 2=CSKH
    public string? Sender { get; set; } 
    
}