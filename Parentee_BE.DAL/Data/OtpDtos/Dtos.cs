namespace Parentee_BE.DAL.Data.OtpDtos;

public record RegisterRequest(string PhoneNumber, string Username, string Password);
public record SendOtpResponse(string TransactionId, long ResendAfterSeconds);
public record VerifyOtpRequest(string TransactionId, string Otp);
public record ApiMessage(string Message);