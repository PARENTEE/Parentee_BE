using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("family")]
public partial class FamilyEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("cover_image_id")]
    public Guid? CoverImageId { get; set; }
    
    [Column("is_disable")]
    public bool IsDisable { get; set; }
    
    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Family")]
    public virtual ICollection<AuditLogEntity> AuditLogs { get; set; } = new List<AuditLogEntity>();

    [InverseProperty("Family")]
    public virtual ICollection<ChildVaccinationEntity> ChildVaccinations { get; set; } = new List<ChildVaccinationEntity>();

    [InverseProperty("Family")]
    public virtual ICollection<ChildEntity> Children { get; set; } = new List<ChildEntity>();

    [ForeignKey("CoverImageId")]
    [InverseProperty("Families")]
    public virtual ImageEntity? CoverImage { get; set; }
    
    [ForeignKey("CreatedBy")]
    [InverseProperty("Family")]
    public virtual UserEntity? CreatedByNavigation { get; set; }

    [InverseProperty("Family")]
    public virtual ICollection<EntitlementEntity> Entitlements { get; set; } = new List<EntitlementEntity>();

    [InverseProperty("Family")]
    public virtual ICollection<ImageEntity> Images { get; set; } = new List<ImageEntity>();

    [InverseProperty("Family")]
    public virtual ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();

    [InverseProperty("Family")]
    public virtual ICollection<NotificationOutboxEntity> NotificationOutboxes { get; set; } = new List<NotificationOutboxEntity>();

    [InverseProperty("Family")]
    public virtual ICollection<PurchaseEntity> Purchases { get; set; } = new List<PurchaseEntity>();
    
    [InverseProperty("Family")]
    public virtual ICollection<UserFamilyRoleEntity> UserFamilyRoles { get; set; } = new List<UserFamilyRoleEntity>();
}
