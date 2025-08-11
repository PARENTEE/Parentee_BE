using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Parentee_BE.BLL.Helpers;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDto.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Parentee_BE.BLL.Services.Implements;

public class AuthService
    (IUnitOfWork<AppDbContext> unitOfWork, ILogger<AuthService> logger,  TokenHelper tokenHelper)
    : BaseService<AuthService>(unitOfWork, logger), IAuthService
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public async Task<string> HandleLogin(LoginRequestDTO loginRequest)
    {
        // Check if account exists
        var account = await _unitOfWork.GetRepository<AccountEntity>()
            .FirstOrDefaultAsync(
                predicate: a => a.Email == loginRequest.Email,
                include: a => a.Include(a => a.RoleEntity)
                );
        if (account == null)
            throw new UnauthorizedAccessException("Invalid email or password");
        
        // Verify password
        var verificationResult = _passwordHasher.VerifyHashedPassword(null, account.Password, loginRequest.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid email or password");
        
        var roleName = account.RoleEntity.Name;
        return tokenHelper.GenerateToken(account.Id.ToString(), account.Email, roleName);
    }
}