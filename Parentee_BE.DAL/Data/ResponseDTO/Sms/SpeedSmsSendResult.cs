namespace Parentee_BE.DAL.Data.ResponseDTO.Sms;

public record SpeedSmsSendResult(long TranId, int TotalSms, decimal TotalPrice, IReadOnlyList<string> InvalidPhone);