namespace Parentee_BE.BLL.Services.Interfaces.OtpInterface;

public interface ISmsSender
{
    Task SendAsync(string to, string body);
}