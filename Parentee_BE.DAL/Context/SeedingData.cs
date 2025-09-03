using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Context;

public static class SeedingData
{
    public static readonly List<UserEntity> Users =
    [
        new UserEntity
        {
            Id = Guid.NewGuid(),
            FullName = "Dad Tester",
            Email = "dadtester123@example.com",
            Password = "Dadtester@123", 
            IsPremium = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new UserEntity
        {
            Id = Guid.NewGuid(),
            FullName = "Mom Tester",
            Email = "momtester123@example.com",
            Password = "Momtester@123", 
            IsPremium = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    ];

    private static readonly List<FamilyEntity> Families =
    [
        new FamilyEntity
        {
            Id = Guid.NewGuid(),
            Name = "Family Tester",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    ];
    
    private static readonly List<UserFamilyRoleEntity> UserFamilyRoles =
    [
        new () { UserId = Users[0].Id, FamilyId = Families.First().Id, Role = FamilyRole.Father, CreatedAt = DateTime.UtcNow},
        new () { UserId = Users[1].Id, FamilyId = Families.First().Id, Role = FamilyRole.Mother, CreatedAt = DateTime.UtcNow},
    ];

    private static readonly List<ProductEntity> Products =
    [
        new ProductEntity
        {
            Code = "premium",
            Name = "Parentee Premium",
            Prices = new List<PriceEntity>
            {
                new () { Id = Guid.NewGuid(), PriceType = PriceType.RecurringMonth, Amount = 129000, Currency = "VND", IsActive = true, CreatedAt = DateTime.UtcNow },
                new () { Id = Guid.NewGuid(), PriceType = PriceType.RecurringMonth, Amount = 999000, Currency = "VND", IsActive = true, CreatedAt = DateTime.UtcNow },
                new () { Id = Guid.NewGuid(), PriceType = PriceType.RecurringMonth, Amount = 4990000, Currency = "VND", IsActive = true, CreatedAt = DateTime.UtcNow }
            },
            Description = "Mở khoá tính năng nâng cao"
        }
    ];

    private static readonly List<VaccineCatalogEntity> Vaccines =
    [
        new VaccineCatalogEntity
        {
            Code = "BCG", Name = "BCG", Description = "Phòng lao",
            RecommendedAgeDays = 0, Doses = 1
        },
        new VaccineCatalogEntity
        {
            Code = "HepB1", Name = "Viêm gan B mũi 1",
            Description = "Tiêm sau sinh", RecommendedAgeDays = 0, Doses = 3
        }
    ];
    
    private static void AddIfNotExists<TEntity>(DbContext context, List<TEntity> records) where TEntity : class
    {
        var existing = context.Set<TEntity>().Any();
        if (!existing) context.Set<TEntity>().AddRange(records);
    }

    
    private static async Task AddIfNotExistsAsync<TEntity>(DbContext context, List<TEntity> records) where TEntity : class
    {
        var existing = await context.Set<TEntity>().AnyAsync();
        if (!existing) await context.Set<TEntity>().AddRangeAsync(records);
    }
    
    public static void Seed(DbContext context)
    {
        AddIfNotExists(context, Users);
        AddIfNotExists(context, Families);
        AddIfNotExists(context, UserFamilyRoles);
        AddIfNotExists(context, Products);
        AddIfNotExists(context, Vaccines);

        context.SaveChanges();
    }
    
    public static async Task SeedAsync(DbContext context, CancellationToken cancellationToken = default)
    {
        await AddIfNotExistsAsync(context, Users);
        await AddIfNotExistsAsync(context, Families);
        await AddIfNotExistsAsync(context, UserFamilyRoles);
        await AddIfNotExistsAsync(context, Products);
        await AddIfNotExistsAsync(context, Vaccines);

        await context.SaveChangesAsync(cancellationToken);
    }
}
