using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("user")]
[Index("Email", Name = "user_email_key", IsUnique = true)]
public partial class UserEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("full_name")]
    public string? FullName { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }
    
    [Column("password")]
    public string? Password { get; set; }
    
    [Column("signup_method")]
    public SigninMethod SigninMethod { get; set; }
    
    [Column("gender")]
    public  Gender Gender { get; set; }
    
    [Column("dob")]
    public DateOnly Dob { get; set; }

    [Column("avatar_image_id")]
    public Guid? AvatarImageId { get; set; }

    [Column("is_premium")]
    public bool IsPremium { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AuditLogEntity> AuditLogs { get; set; } = new List<AuditLogEntity>();

    [InverseProperty("User")]
    public virtual ICollection<AuthIdentityEntity> AuthIdentities { get; set; } = new List<AuthIdentityEntity>();

    [ForeignKey("AvatarImageId")]
    [InverseProperty("Users")]
    public virtual ImageEntity? AvatarImage { get; set; }
    
    [InverseProperty("CreatedByNavigation")]
    public virtual FamilyEntity Family { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<ChildVaccinationEntity> ChildVaccinationCreatedByNavigations { get; set; } = new List<ChildVaccinationEntity>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<ChildVaccinationEntity> ChildVaccinationUpdatedByNavigations { get; set; } = new List<ChildVaccinationEntity>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<DiaperChangeEntity> DiaperChanges { get; set; } = new List<DiaperChangeEntity>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<SolidFoodEntity> SolidFood { get; set; } = new List<SolidFoodEntity>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<FeedingEntity> Feedings { get; set; } = new List<FeedingEntity>();

    [InverseProperty("OwnerUser")]
    public virtual ICollection<ImageEntity> Images { get; set; } = new List<ImageEntity>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();

    [InverseProperty("User")]
    public virtual ICollection<NotificationOutboxEntity> NotificationOutboxes { get; set; } = new List<NotificationOutboxEntity>();

    [InverseProperty("User")]
    public virtual ICollection<PurchaseEntity> Purchases { get; set; } = new List<PurchaseEntity>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<SleepEntity> Sleeps { get; set; } = new List<SleepEntity>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<TaskEntity> TaskCreatedByNavigations { get; set; } = new List<TaskEntity>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<TaskEntity> TaskUpdatedByNavigations { get; set; } = new List<TaskEntity>();

    [InverseProperty("User")]
    public virtual UserFamilyRoleEntity? UserFamilyRole { get; set; }
}
