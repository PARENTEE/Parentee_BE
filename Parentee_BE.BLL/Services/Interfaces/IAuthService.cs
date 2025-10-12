
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Parentee_BE.DAL.Data.RequestDto.Auth;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<string> HandleLogin(LoginRequestDTO loginRequestDto);

    Task<string> HandleGoogleLogin(string email, string fullName);
}