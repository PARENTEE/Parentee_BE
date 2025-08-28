using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("purchase")]
public partial class PurchaseEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("family_id")]
    public Guid? FamilyId { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("price_id")]
    public Guid? PriceId { get; set; }
    
    [Column("payment_method")]
    public PaymentMethod PaymentMethod { get; set; }

    [Column("provider_txn_id")]
    public string? ProviderTxnId { get; set; }
    
    [Column("status")]
    public PurchaseStatus Status { get; set; }

    [Column("amount")]
    [Precision(12, 2)]
    public decimal Amount { get; set; }

    [Column("currency")]
    public string Currency { get; set; } = null!;

    [Column("raw_payload", TypeName = "jsonb")]
    public string? RawPayload { get; set; }

    [Column("paid_at")]
    public DateTime? PaidAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Purchase")]
    public virtual ICollection<EntitlementEntity> Entitlements { get; set; } = new List<EntitlementEntity>();

    [ForeignKey("FamilyId")]
    [InverseProperty("Purchases")]
    public virtual FamilyEntity? Family { get; set; }

    [InverseProperty("Purchase")]
    public virtual ICollection<InvoiceEntity> Invoices { get; set; } = new List<InvoiceEntity>();

    [ForeignKey("PriceId")]
    [InverseProperty("Purchases")]
    public virtual PriceEntity? Price { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Purchases")]
    public virtual ProductEntity Product { get; set; } = null!;

    [InverseProperty("Purchase")]
    public virtual ICollection<RefundEntity> Refunds { get; set; } = new List<RefundEntity>();

    [ForeignKey("UserId")]
    [InverseProperty("Purchases")]
    public virtual UserEntity User { get; set; } = null!;
}
