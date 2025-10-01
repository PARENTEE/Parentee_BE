using Parentee_BE.DAL.Data.SmsDTO;

namespace Parentee_BE.DAL.Data.ResponseDTO.Sms;

public class SpeedSmsSendResponse
{
    public string Status { get; set; } = default!; // "success" | "error"
    public string? Code { get; set; }
    public string? Message { get; set; }
    public SpeedSmsSendData? Data { get; set; }
}