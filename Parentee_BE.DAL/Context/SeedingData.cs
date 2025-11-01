using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using TaskStatus = Parentee_BE.DAL.Data.Enums.TaskStatus;

namespace Parentee_BE.DAL.Context;

public static class SeedingData
{
    #region Data to seed

    public static readonly List<UserEntity> Users =
    [
        new UserEntity
        {
            Id = Guid.NewGuid(),
            FullName = "Dad Tester",    
            Email = "dadtester123@example.com",
            Password = "Dadtester@123", 
            Gender = Gender.Male,
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
            Gender = Gender.Male,
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
        new () { UserId = Users[0].Id, FamilyId = Families.First().Id, Role = FamilyRole.Father, InvitationStatus = InvitationStatus.Accepted, CreatedAt = DateTime.UtcNow},
        new () { UserId = Users[1].Id, FamilyId = Families.First().Id, Role = FamilyRole.Mother, InvitationStatus = InvitationStatus.Accepted, CreatedAt = DateTime.UtcNow},
    ];

    #region Child and Other Data

    private static readonly List<ChildEntity> ChildEntities = 
    [ 
        new() { Id = Guid.NewGuid(), FamilyId = Families.First().Id, FullName = "Emma Johnson", BirthDate = new DateOnly(2023, 5, 15), Gender = Gender.Female, Notes = "First child of the Johnson family", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }, 
        new() { Id = Guid.NewGuid(), FamilyId = Families.First().Id, FullName = "Noah Johnson", BirthDate = new DateOnly(2025, 1, 20), Gender = Gender.Male, Notes = "Younger brother, born recently", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow } 
    ];
    
    private static readonly List<MeasurementEntity> MeasurementEntities = 
    [ 
        new() { Id = Guid.NewGuid(), FamilyId = Families.First().Id, ChildId = ChildEntities.First().Id, Type = MeasureType.Weight, MeasuredAt = DateTime.UtcNow.AddDays(-1), Value = 6.80m, Unit = "kg", Source = "Manual", Notes = "Regular checkup", CreatedBy = Users[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }, 
        new() { Id = Guid.NewGuid(), FamilyId = Families.First().Id, ChildId = ChildEntities.First().Id, Type = MeasureType.Length, MeasuredAt = DateTime.UtcNow.AddDays(-1), Value = 65.50m, Unit = "cm", Source = "Manual", Notes = "Monthly growth measurement", CreatedBy = Users[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow } 
    ];
    
    private static readonly List<FeedingEntity> FeedingEntities = 
    [
        new() { Id = Guid.NewGuid(), ChildId = ChildEntities.First().Id, Method = FeedingMethod.Bottle, StartedAt = DateTime.UtcNow.AddHours(-4), EndedAt = DateTime.UtcNow.AddHours(-3).AddMinutes(-45), LeftDuration = new TimeSpan(0, 1, 11), RightDuration = new TimeSpan(0, 1, 11), Notes = "Morning bottle feeding", CreatedBy = Users[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new() { Id = Guid.NewGuid(), ChildId = ChildEntities.First().Id, Method = FeedingMethod.Breast, StartedAt = DateTime.UtcNow.AddHours(-2), EndedAt = DateTime.UtcNow.AddHours(-1).AddMinutes(-30), LeftDuration = new TimeSpan(0,1, 11), RightDuration = new TimeSpan(0, 1, 11), Notes = "Afternoon breastfeeding", CreatedBy = Users[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
    ];
    
    private static readonly List<DiaperChangeEntity> DiaperChangeEntities = 
    [
        new() { Id = Guid.NewGuid(), ChildId = ChildEntities.First().Id, ChangedAt = DateTime.UtcNow.AddHours(-6), Type = DiaperType.Pee, DiaperQuantity = DiaperQuantity.Small, Notes = "Morning diaper change", CreatedBy = Users[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, DiaperWaste = DiaperWaste.Loose},
        new() { Id = Guid.NewGuid(), ChildId = ChildEntities.First().Id, ChangedAt = DateTime.UtcNow.AddHours(-2), Type = DiaperType.Poo, DiaperQuantity = DiaperQuantity.Small, Notes = "Afternoon change with slight rash", CreatedBy = Users[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, DiaperWaste = DiaperWaste.Loose }
    ];
    
    private static readonly List<SleepEntity> SleepEntities = 
    [
        new() { Id = Guid.NewGuid(), ChildId = ChildEntities.First().Id, StartedAt = DateTime.UtcNow.AddHours(-10), EndedAt = DateTime.UtcNow.AddHours(-8), Location = "Crib", Notes = "Morning nap", CreatedBy = Users[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new() { Id = Guid.NewGuid(), ChildId = ChildEntities.First().Id, StartedAt = DateTime.UtcNow.AddHours(-5), EndedAt = DateTime.UtcNow.AddHours(-3), Location = "Stroller", Notes = "Afternoon nap", CreatedBy = Users[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
    ];
    
    #endregion
    
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
        
    private static readonly List<TaskEntity> TaskEntities =
    [
        // Yesterday's tasks
        new TaskEntity
        {
            Id = Guid.NewGuid(),
            ChildId = ChildEntities.First().Id,
            Title = "Cho bé bú sáng",
            StartsAt = DateTime.UtcNow.Date.AddDays(-1).AddHours(8),
            EndsAt = DateTime.UtcNow.Date.AddDays(-1).AddHours(8).AddMinutes(30),
            Status = TaskStatus.Completed,
            CreatedBy = Users[1].Id, // Mom
            UpdatedBy = Users[1].Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new TaskEntity
        {
            Id = Guid.NewGuid(),
            ChildId = ChildEntities.First().Id,
            Title = "Thay tã buổi chiều",
            StartsAt = DateTime.UtcNow.Date.AddDays(-1).AddHours(15),
            EndsAt = DateTime.UtcNow.Date.AddDays(-1).AddHours(15).AddMinutes(15),
            Status = TaskStatus.Completed,
            CreatedBy = Users[0].Id, // Dad
            UpdatedBy = Users[0].Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },

        // Today's tasks
        new TaskEntity
        {
            Id = Guid.NewGuid(),
            ChildId = ChildEntities.First().Id,
            Title = "Cho bé bú sáng",
            StartsAt = DateTime.UtcNow.Date.AddHours(8),
            EndsAt = DateTime.UtcNow.Date.AddHours(8).AddMinutes(20),
            Status = TaskStatus.Completed,
            CreatedBy = Users[1].Id, // Mom
            UpdatedBy = Users[1].Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new TaskEntity
        {
            Id = Guid.NewGuid(),
            ChildId = ChildEntities.First().Id,
            Title = "Chơi với bé",
            StartsAt = DateTime.UtcNow.Date.AddHours(14),
            EndsAt = DateTime.UtcNow.Date.AddHours(15),
            Status = TaskStatus.Pending,
            CreatedBy = Users[0].Id, // Dad
            UpdatedBy = Users[0].Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new TaskEntity
        {
            Id = Guid.NewGuid(),
            ChildId = ChildEntities.First().Id,
            Title = "Tắm cho bé",
            StartsAt = DateTime.UtcNow.Date.AddHours(17),
            EndsAt = DateTime.UtcNow.Date.AddHours(17).AddMinutes(30),
            Status = TaskStatus.Pending,
            CreatedBy = Users[1].Id,
            UpdatedBy = Users[1].Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },

        // Tomorrow’s tasks
        new TaskEntity
        {
            Id = Guid.NewGuid(),
            ChildId = ChildEntities.First().Id,
            Title = "Massage cho bé",
            StartsAt = DateTime.UtcNow.Date.AddDays(1).AddHours(18),
            EndsAt = DateTime.UtcNow.Date.AddDays(1).AddHours(18).AddMinutes(20),
            Status = TaskStatus.Pending,
            CreatedBy = Users[0].Id,
            UpdatedBy = Users[0].Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    ];


    #endregion
    
    
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
        AddIfNotExists(context, ChildEntities);
        context.SaveChanges();
        

        AddIfNotExists(context, MeasurementEntities);
        AddIfNotExists(context, DiaperChangeEntities);
        AddIfNotExists(context, FeedingEntities);
        AddIfNotExists(context, SleepEntities);
        
        AddIfNotExists(context, Products);
        AddIfNotExists(context, Vaccines);
        AddIfNotExists(context, TaskEntities);
        

        context.SaveChanges();
    }
    
    public static async Task SeedAsync(DbContext context, CancellationToken cancellationToken = default)
    {
        await AddIfNotExistsAsync(context, Users);
        await AddIfNotExistsAsync(context, Families);
        await AddIfNotExistsAsync(context, UserFamilyRoles);
        await AddIfNotExistsAsync(context, ChildEntities);

        await context.SaveChangesAsync(cancellationToken);
        
        await AddIfNotExistsAsync(context, MeasurementEntities);
        await AddIfNotExistsAsync(context, DiaperChangeEntities);
        await AddIfNotExistsAsync(context, FeedingEntities);
        await AddIfNotExistsAsync(context, SleepEntities);
        
        await AddIfNotExistsAsync(context, Products);
        await AddIfNotExistsAsync(context, Vaccines);
        await AddIfNotExistsAsync(context, TaskEntities);

        await context.SaveChangesAsync(cancellationToken);
    }
}
