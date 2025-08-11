
using Parentee_BE.DAL.Data.RequestDto.Auth;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<string> HandleLogin(LoginRequestDTO loginRequestDto);
}