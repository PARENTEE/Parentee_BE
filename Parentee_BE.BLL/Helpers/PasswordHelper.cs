using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Parentee_BE.BLL.Helpers;

public class PasswordHelper
{
    private static readonly PasswordHasher<object> PasswordHasher = new();

    // Simple SHA256 hasher (you can replace with ASP.NET Identity PasswordHasher if needed)
    public static string HashPassword(string password)
    {
        return PasswordHasher.HashPassword(null, password);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var result = PasswordHasher.VerifyHashedPassword(null, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}