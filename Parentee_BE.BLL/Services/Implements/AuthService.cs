using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Parentee_BE.BLL.Helpers;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDto.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.BLL.Services.Implements;

public class AuthService
    (IUnitOfWork<AppDbContext> unitOfWork, ILogger<AuthService> logger,  TokenHelper tokenHelper)
    : BaseService<AuthService>(unitOfWork, logger), IAuthService
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public async Task<string> HandleLogin(LoginRequestDTO loginRequest)
    {
        // Check if account exists
        var account = await _unitOfWork.GetRepository<UserEntity>()
            .FirstOrDefaultAsync(
                predicate: a => a.Email == loginRequest.Email,
                include: a => a.Include(a => a.UserFamilyRole)
                );
        if (account == null)
            throw new UnauthorizedAccessException("Invalid email!");
        
        // Verify password
        var verificationResult = PasswordHelper.VerifyPassword(loginRequest.Password, account.Password);
        if (!verificationResult)
            throw new UnauthorizedAccessException("Invalid password!");
        
        var roleName = account.UserFamilyRole?.Role.ToString() ?? "None";
        return tokenHelper.GenerateToken(account.Id.ToString(), account.Email, roleName);
    }
    
    
    public async Task<string> HandleGoogleLogin(GoogleJsonWebSignature.Payload payload)
    {
        var email = payload.Email;
        var fullName = payload.Name;
        var givenName = payload.GivenName;
        var surname = payload.Name;
        
        // Check if account exists
        var account = await _unitOfWork.GetRepository<UserEntity>()
            .FirstOrDefaultAsync(
                predicate: a => a.Email == email,
                include: a => a.Include(a => a.UserFamilyRole)
            );
        
        // If not exist, create one 
        if (account == null)
        {
            account = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = email,
                FullName = fullName,
                IsActive = true,
                SigninMethod = SigninMethod.Google,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.GetRepository<UserEntity>().InsertAsync(account);
        }
        
        var roleName = account.UserFamilyRole?.Role.ToString() ?? "None";
        return tokenHelper.GenerateToken(account.Id.ToString(), account.Email, roleName);
    }
    
}