using Parentee_BE.DAL.Data.ResponseDTO.Sms;
using Parentee_BE.DAL.Data.SmsDTO;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface ISmsSender
{
    Task<SpeedSmsSendResult> SendAsync(
        IEnumerable<string> to,
        string content,
        int? smsType = null,
        string? sender = null,
        CancellationToken ct = default);

    Task<IReadOnlyList<SpeedSmsStatusItem>> GetStatusAsync(
        long tranId,
        CancellationToken ct = default);
}