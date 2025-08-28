using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("entitlement")]
[Index("FamilyId", "ProductId", "StartsAt", Name = "entitlement_family_id_product_id_starts_at_key", IsUnique = true)]
public partial class EntitlementEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("family_id")]
    public Guid FamilyId { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("purchase_id")]
    public Guid? PurchaseId { get; set; }
    
    [Column("starts_at")]
    public DateTime StartsAt { get; set; }

    [Column("status")]
    public EntitlementStatus Status { get; set; }

    [Column("ends_at")]
    public DateTime? EndsAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("FamilyId")]
    [InverseProperty("Entitlements")]
    public virtual FamilyEntity Family { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Entitlements")]
    public virtual ProductEntity Product { get; set; } = null!;

    [ForeignKey("PurchaseId")]
    [InverseProperty("Entitlements")]
    public virtual PurchaseEntity? Purchase { get; set; }
}
