using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("product")]
[Index("Code", Name = "product_code_key", IsUnique = true)]
public partial class ProductEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("code")]
    public string Code { get; set; } = null!;

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<EntitlementEntity> Entitlements { get; set; } = new List<EntitlementEntity>();

    [InverseProperty("Product")]
    public virtual ICollection<PriceEntity> Prices { get; set; } = new List<PriceEntity>();

    [InverseProperty("Product")]
    public virtual ICollection<PurchaseEntity> Purchases { get; set; } = new List<PurchaseEntity>();
}
