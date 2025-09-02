using Parentee_BE.DAL.Data.OtpDtos;

namespace Parentee_BE.BLL.Services.Interfaces.OtpInterface;

public interface IOtpStore
{
    Task<OtpRecord?> GetAsync(string key);
    Task PutAsync(string key, OtpRecord value, TimeSpan ttl);
    Task DeleteAsync(string key);
}