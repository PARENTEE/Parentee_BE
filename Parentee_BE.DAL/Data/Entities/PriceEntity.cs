using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("price")]
public partial class PriceEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }
    
    [Column("price_type")]
    public PriceType PriceType { get; set; }

    [Column("amount")]
    [Precision(12, 2)]
    public decimal Amount { get; set; }

    [Column("currency")]
    public string Currency { get; set; } = null!;

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("provider_price_id")]
    public string? ProviderPriceId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Prices")]
    public virtual ProductEntity Product { get; set; } = null!;

    [InverseProperty("Price")]
    public virtual ICollection<PurchaseEntity> Purchases { get; set; } = new List<PurchaseEntity>();
}
