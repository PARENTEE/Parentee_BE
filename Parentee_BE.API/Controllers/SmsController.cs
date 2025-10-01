using Microsoft.AspNetCore.Mvc;
using Parentee_BE.API.Constants;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.Sms;

namespace Parentee_BE.API.Controllers;

public class SmsController(ISmsSender _smsSender, ILogger<SmsController> _logger) : BaseController<SmsController>(_logger)
{
    [HttpPost(APIEndpointsConstant.SmsEndpoints.SMS_ENDPOINT)]
    public async Task<IActionResult> Send([FromBody] SendSmsRequest req, CancellationToken ct)
    {
        var result = await _smsSender.SendAsync(req.To, req.Content, req.SmsType, req.Sender, ct);
        return Ok(new { result.TranId, result.TotalSms, result.TotalPrice, result.InvalidPhone });
    }
}