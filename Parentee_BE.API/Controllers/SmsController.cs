using System.Text;
using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.RequestDTO.Sms;

namespace Parentee_BE.API.Controllers;

public class SmsController(ISmsSender _smsSender, ILogger<SmsController> _logger) : BaseController<SmsController>(_logger)
{
 

    // POST /api/sms/test
    [HttpPost("test")]
    public async Task<IActionResult> Send([FromBody] SendSmsRequest req, CancellationToken ct)
    {
        // Khuyến nghị format số ở dạng quốc tế: "84xxxxxxxxx" (ví dụ +1208..., +49..., …) :contentReference[oaicite:13]{index=13}
        var result = await _smsSender.SendAsync(req.To, req.Content, req.SmsType, "5fa2f6f5639b7b6d", ct);
        return Ok(new { result.TranId, result.TotalSms, result.TotalPrice, result.InvalidPhone });
    }

    public sealed class SendSmsRequest
    {
        public List<string> To { get; set; } = new();
        public string Content { get; set; } = default!;
        public int? SmsType { get; set; } // 2/3/4/5/6
        public string? Sender { get; set; } // brandname nếu smsType=3
    }
    
    private static string NormalizePhone(string raw)
    {
        var s = new string(raw.Where(char.IsDigit).ToArray());
        if (s.StartsWith("0")) s = "84" + s.Substring(1);
        if (s.StartsWith("84")) return s;
        // nếu đã là số quốc tế khác Việt Nam, bạn có thể giữ nguyên hoặc kiểm tra sâu hơn
        return s;
    }

}