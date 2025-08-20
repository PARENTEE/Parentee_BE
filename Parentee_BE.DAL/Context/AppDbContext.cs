using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

public class AppDbContext : DbContext
{
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    
    public AppDbContext() { }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSeeding((context, _) => SeedingData.Seed(context))
            .UseAsyncSeeding(
                async (context, _, cancellationToken) => await SeedingData.SeedAsync(context, cancellationToken)
            );
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<RoleEntity>()
            .HasKey(r => r.Id);
        modelBuilder.Entity<RoleEntity>()
            .Property(r => r.Name)
            .HasMaxLength(50).IsRequired();
        modelBuilder.Entity<RoleEntity>()
            .HasData(
                new RoleEntity { Id = 1, Name = "Admin" },
                new RoleEntity { Id = 2, Name = "User" }
            );

        
        modelBuilder.Entity<AccountEntity>()
            .HasOne(a => a.RoleEntity)
            .WithMany()
            .HasForeignKey(a => a.RoleId);
    }
}