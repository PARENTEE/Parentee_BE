using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Parentee_BE.DAL.Data.Entities;

public static class SeedingData
{
   
    private static readonly List<RoleEntity> Roles =
    [
        new RoleEntity { Id = RoleEntity.AdminId, Name = RoleEntity.Admin },
        new RoleEntity { Id = RoleEntity.UserId,  Name = RoleEntity.User }
    ];

    public static readonly List<AccountEntity> Accounts =
    [
        new AccountEntity
        {
            FirstName = "Admin",
            LastName = "System",
            Email = "admin@example.com",
            Password = "Admin@123",
            RoleId = RoleEntity.AdminId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new AccountEntity
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "user@example.com",
            Password = "User@123",
            RoleId = RoleEntity.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    ];

    private static async Task AddIfNotExistsAsync<TEntity>(DbContext context, List<TEntity> records) where TEntity : class
    {
        if (!await context.Set<TEntity>().AnyAsync())
        {
            await context.Set<TEntity>().AddRangeAsync(records);
        }
    }

    private static void AddIfNotExists<TEntity>(DbContext context, List<TEntity> records) where TEntity : class
    {
        if (!context.Set<TEntity>().Any())
        {
            context.Set<TEntity>().AddRange(records);
        }
    }

    public static async Task SeedAsync(DbContext context, CancellationToken cancellationToken = default)
    {
        await AddIfNotExistsAsync(context, Roles);
        await AddIfNotExistsAsync(context, Accounts);
        await context.SaveChangesAsync(cancellationToken);
    }

    public static void Seed(DbContext context)
    {
        AddIfNotExists(context, Roles);
        AddIfNotExists(context, Accounts);
        context.SaveChanges();
    }
}
