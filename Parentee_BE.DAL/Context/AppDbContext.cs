using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using TaskStatus = Parentee_BE.DAL.Data.Enums.TaskStatus;

namespace Parentee_BE.DAL.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLogEntity> AuditLogs { get; set; }

    public virtual DbSet<AuthIdentityEntity> AuthIdentities { get; set; }

    public virtual DbSet<CalendarEventEntity> CalendarEvents { get; set; }

    public virtual DbSet<ChildEntity> Children { get; set; }

    public virtual DbSet<ChildPhotoEntity> ChildPhotos { get; set; }

    public virtual DbSet<ChildVaccinationEntity> ChildVaccinations { get; set; }

    public virtual DbSet<DiaperChangeEntity> DiaperChanges { get; set; }

    public virtual DbSet<EntitlementEntity> Entitlements { get; set; }

    public virtual DbSet<FamilyEntity> Families { get; set; }

    public virtual DbSet<FeedingEntity> Feedings { get; set; }

    public virtual DbSet<ImageEntity> Images { get; set; }

    public virtual DbSet<InvoiceEntity> Invoices { get; set; }

    public virtual DbSet<MeasurementEntity> Measurements { get; set; }

    public virtual DbSet<NotificationOutboxEntity> NotificationOutboxes { get; set; }

    public virtual DbSet<PriceEntity> Prices { get; set; }

    public virtual DbSet<ProductEntity> Products { get; set; }

    public virtual DbSet<PurchaseEntity> Purchases { get; set; }

    public virtual DbSet<RefundEntity> Refunds { get; set; }

    public virtual DbSet<ReminderEntity> Reminders { get; set; }

    public virtual DbSet<SleepEntity> Sleeps { get; set; }

    public virtual DbSet<TaskEntity> Tasks { get; set; }

    public virtual DbSet<TaskRecurrenceEntity> TaskRecurrences { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    public virtual DbSet<UserFamilyRoleEntity> UserFamilyRoles { get; set; }

    public virtual DbSet<VaccineCatalogEntity> VaccineCatalogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Enums

        // User
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.Property(e => e.SigninMethod)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
        });

        // Feeding
        modelBuilder.Entity<FeedingEntity>(entity =>
        {
            entity.Property(e => e.Method)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
        });

        // Diaper Change
        modelBuilder.Entity<DiaperChangeEntity>(entity =>
        {
            entity.Property(e => e.Type)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
            
            entity.Property(e => e.DiaperWaste)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
            
            entity.Property(e => e.DiaperQuantity)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
            
        });
        
        // Diaper Change
        modelBuilder.Entity<SolidFoodEntity>(entity =>
        {
            entity.Property(e => e.Unit)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
        });

        // Measurement
        modelBuilder.Entity<MeasurementEntity>(entity =>
        {
            entity.Property(e => e.Type)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
        });

        // Child Vaccination
        modelBuilder.Entity<ChildVaccinationEntity>(entity =>
        {
            entity.Property(e => e.Status)
                .HasColumnType("text")
                .HasConversion<string>()
                .HasDefaultValue(VaccinationStatus.Scheduled);
        });

        // Task
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.Property(e => e.Status)
                .HasColumnType("text")
                .HasConversion<string>()
                .HasDefaultValue(TaskStatus.Pending);
        });

        // Reminder
        modelBuilder.Entity<ReminderEntity>(entity =>
        {
            entity.Property(e => e.Channel)
                .HasColumnType("text")
                .HasConversion<string>()
                .HasDefaultValue(ReminderChannel.Push);
        });

        // Notification Outbox
        modelBuilder.Entity<NotificationOutboxEntity>(entity =>
        {
            entity.Property(e => e.Channel)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
        });

        // Price
        modelBuilder.Entity<PriceEntity>(entity =>
        {
            entity.Property(e => e.PriceType)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
        });

        // Purchase
        modelBuilder.Entity<PurchaseEntity>(entity =>
        {
            entity.Property(e => e.Status)
                .HasColumnType("text")
                .HasConversion<string>()
                .HasDefaultValue(PurchaseStatus.Pending);

            entity.Property(e => e.PaymentMethod)
                .HasColumnType("text")
                .HasConversion<string>()
                .HasDefaultValue(PaymentMethod.CreditCard);
        });

        // Entitlement
        modelBuilder.Entity<EntitlementEntity>(entity =>
        {
            entity.Property(e => e.Status)
                .HasColumnType("text")
                .HasConversion<string>()
                .HasDefaultValue(EntitlementStatus.Active);
        });

        // User Family Role
        modelBuilder.Entity<UserFamilyRoleEntity>(entity =>
        {
            entity.Property(e => e.Role)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
        });
        
        modelBuilder.Entity<UserFamilyRoleEntity>(entity =>
        {
            entity.Property(e => e.InvitationStatus)
                .HasColumnType("text")
                .HasConversion<string>()
                .IsRequired();
        });

        #endregion

        modelBuilder.Entity<AuditLogEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_log_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Family).WithMany(p => p.AuditLogs).HasConstraintName("audit_log_family_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs).HasConstraintName("audit_log_user_id_fkey");
        });

        modelBuilder.Entity<AuthIdentityEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_identity_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.User).WithMany(p => p.AuthIdentities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_identity_user_id_fkey");
        });

        modelBuilder.Entity<CalendarEventEntity>(entity => { entity.ToView("calendar_event"); });

        modelBuilder.Entity<ChildEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("child_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Family).WithMany(p => p.Children)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("child_family_id_fkey");

            entity.HasOne(d => d.PhotoImage).WithMany(p => p.Children).HasConstraintName("child_photo_image_id_fkey");
        });

        modelBuilder.Entity<ChildPhotoEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("child_photo_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Child).WithMany(p => p.ChildPhotos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("child_photo_child_id_fkey");

            entity.HasOne(d => d.Image).WithMany(p => p.ChildPhotos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("child_photo_image_id_fkey1");
        });

        modelBuilder.Entity<ChildVaccinationEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("child_vaccination_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Child).WithMany(p => p.ChildVaccinations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("child_vaccination_child_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ChildVaccinationCreatedByNavigations)
                .HasConstraintName("child_vaccination_created_by_fkey");

            entity.HasOne(d => d.Family).WithMany(p => p.ChildVaccinations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("child_vaccination_family_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ChildVaccinationUpdatedByNavigations)
                .HasConstraintName("child_vaccination_updated_by_fkey");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.ChildVaccinations)
                .HasConstraintName("child_vaccination_vaccine_id_fkey");
        });

        modelBuilder.Entity<DiaperChangeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("diaper_change_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Child).WithMany(p => p.DiaperChanges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("diaper_change_child_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DiaperChanges)
                .HasConstraintName("diaper_change_created_by_fkey");
        });
        
        modelBuilder.Entity<SolidFoodEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("solid_food_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Child).WithMany(p => p.SolidFood)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solid_food_child_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SolidFood)
                .HasConstraintName("solid_food_created_by_fkey");
        });

        modelBuilder.Entity<EntitlementEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entitlement_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Family).WithMany(p => p.Entitlements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entitlement_family_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Entitlements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entitlement_product_id_fkey");

            entity.HasOne(d => d.Purchase).WithMany(p => p.Entitlements)
                .HasConstraintName("entitlement_purchase_id_fkey");
        });

        modelBuilder.Entity<FamilyEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("family_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.CoverImage).WithMany(p => p.Families).HasConstraintName("family_cover_image_id_fkey");
        });

        modelBuilder.Entity<FeedingEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("feeding_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Child).WithMany(p => p.Feedings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("feeding_child_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Feedings)
                .HasConstraintName("feeding_created_by_fkey");
        });

        modelBuilder.Entity<ImageEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("image_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Family).WithMany(p => p.Images).HasConstraintName("fk_image_family");

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.Images).HasConstraintName("fk_image_owner");
        });

        modelBuilder.Entity<InvoiceEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoice_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Currency).HasDefaultValueSql("'VND'::text");

            entity.HasOne(d => d.PdfImage).WithMany(p => p.Invoices).HasConstraintName("invoice_pdf_image_id_fkey");

            entity.HasOne(d => d.Purchase).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_purchase_id_fkey");
        });

        modelBuilder.Entity<MeasurementEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("measurement_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Child).WithMany(p => p.Measurements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("measurement_child_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Measurements)
                .HasConstraintName("measurement_created_by_fkey");

            entity.HasOne(d => d.Family).WithMany(p => p.Measurements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("measurement_family_id_fkey");
        });

        modelBuilder.Entity<NotificationOutboxEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notification_outbox_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Attempts).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Family).WithMany(p => p.NotificationOutboxes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notification_outbox_family_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationOutboxes)
                .HasConstraintName("notification_outbox_user_id_fkey");
        });

        modelBuilder.Entity<PriceEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("price_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Currency).HasDefaultValueSql("'VND'::text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Product).WithMany(p => p.Prices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("price_product_id_fkey");
        });

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<PurchaseEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Currency).HasDefaultValueSql("'VND'::text");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Family).WithMany(p => p.Purchases).HasConstraintName("purchase_family_id_fkey");

            entity.HasOne(d => d.Price).WithMany(p => p.Purchases).HasConstraintName("purchase_price_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Purchases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_product_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Purchases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_user_id_fkey");
        });

        modelBuilder.Entity<RefundEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("refund_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Currency).HasDefaultValueSql("'VND'::text");
            entity.Property(e => e.RefundedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Purchase).WithMany(p => p.Refunds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refund_purchase_id_fkey");
        });

        modelBuilder.Entity<ReminderEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reminder_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.Task).WithMany(p => p.Reminders).HasConstraintName("reminder_task_id_fkey");
        });

        modelBuilder.Entity<SleepEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sleep_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Child).WithMany(p => p.Sleeps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sleep_child_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Sleeps)
                .HasConstraintName("sleep_created_by_fkey");
        });

        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("task_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.AllDay).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Child).WithMany(p => p.Tasks).HasConstraintName("task_child_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TaskCreatedByNavigations)
                .HasConstraintName("task_created_by_fkey");
            
            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TaskUpdatedByNavigations)
                .HasConstraintName("task_updated_by_fkey");
        });

        modelBuilder.Entity<TaskRecurrenceEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("task_recurrence_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskRecurrences)
                .HasConstraintName("task_recurrence_task_id_fkey");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsPremium).HasDefaultValue(false);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.AvatarImage).WithMany(p => p.Users).HasConstraintName("user_avatar_image_id_fkey");
        });

        modelBuilder.Entity<UserFamilyRoleEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_family_role_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Family).WithMany(p => p.UserFamilyRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_family_role_family_id_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.UserFamilyRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_family_role_user_id_fkey");
        });

        modelBuilder.Entity<VaccineCatalogEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vaccine_catalog_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}