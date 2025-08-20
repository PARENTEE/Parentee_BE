using Parentee_BE.BLL.Helpers;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDto.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            throw new UnauthorizedAccessException("Invalid email!");
        
        // Verify password
        var verificationResult = PasswordHelper.VerifyPassword(loginRequest.Password, account.Password);
        if (!verificationResult)
            throw new UnauthorizedAccessException("Invalid password!");
        
        var roleName = account.RoleEntity.Name;
        return tokenHelper.GenerateToken(account.Id.ToString(), account.Email, roleName);
    }
}