using Parentee_BE.DAL.Data.SmsDTO;

namespace Parentee_BE.DAL.Data.ResponseDTO.Sms;

public class SpeedSmsStatusResponse
{
    public string Status { get; set; } = default!;
    public string? Code { get; set; }
    public string? Message { get; set; }
    public List<SpeedSmsStatusItem>? Data { get; set; }
}