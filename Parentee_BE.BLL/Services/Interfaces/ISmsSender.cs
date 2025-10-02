using Parentee_BE.DAL.Data.RequestDTO.Sms;
using Parentee_BE.DAL.Data.ResponseDTO.Sms;
using Parentee_BE.DAL.Data.SmsDTO;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface ISmsSender
{
    Task<string> SendAsync(
        SendSmsRequest data,
        CancellationToken ct = default);

    Task<IReadOnlyList<SpeedSmsStatusItem>> GetStatusAsync(
        long tranId,
        CancellationToken ct = default);
}