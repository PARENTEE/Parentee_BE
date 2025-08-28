using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("refund")]
public partial class RefundEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("purchase_id")]
    public Guid PurchaseId { get; set; }

    [Column("amount")]
    [Precision(12, 2)]
    public decimal Amount { get; set; }

    [Column("currency")]
    public string Currency { get; set; } = null!;

    [Column("reason")]
    public string? Reason { get; set; }

    [Column("provider_refund_id")]
    public string? ProviderRefundId { get; set; }

    [Column("refunded_at")]
    public DateTime RefundedAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("PurchaseId")]
    [InverseProperty("Refunds")]
    public virtual PurchaseEntity Purchase { get; set; } = null!;
}
