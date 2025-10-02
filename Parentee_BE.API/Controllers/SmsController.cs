using System.Text;
using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.RequestDTO.Sms;

namespace Parentee_BE.API.Controllers;

public class SmsController(ISmsSender _smsSender, ILogger<SmsController> _logger) : BaseController<SmsController>(_logger)
{
    [HttpPost("test")]
    public async Task<IActionResult> Send([FromBody] SendSmsRequest req, CancellationToken ct)
    {
        var to = NormalizePhone(req.To);

        var rawJsonResponse = await _smsSender.SendAsync(req, ct);
        return Content(rawJsonResponse, "application/json", Encoding.UTF8);
    }

    private static string NormalizePhone(string raw)
    {
        var digits = new string(raw.Where(char.IsDigit).ToArray());
        if (digits.StartsWith("0")) return "84" + digits[1..];
        return digits;
    }

}