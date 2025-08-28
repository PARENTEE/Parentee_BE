using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("invoice")]
[Index("InvoiceNo", Name = "invoice_invoice_no_key", IsUnique = true)]
public partial class InvoiceEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("purchase_id")]
    public Guid PurchaseId { get; set; }

    [Column("invoice_no")]
    public string? InvoiceNo { get; set; }

    [Column("issued_at")]
    public DateTime? IssuedAt { get; set; }

    [Column("buyer_name")]
    public string? BuyerName { get; set; }

    [Column("buyer_email", TypeName = "citext")]
    public string? BuyerEmail { get; set; }

    [Column("buyer_tax_code")]
    public string? BuyerTaxCode { get; set; }

    [Column("amount_total")]
    [Precision(12, 2)]
    public decimal AmountTotal { get; set; }

    [Column("currency")]
    public string Currency { get; set; } = null!;

    [Column("pdf_image_id")]
    public Guid? PdfImageId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("PdfImageId")]
    [InverseProperty("Invoices")]
    public virtual ImageEntity? PdfImage { get; set; }

    [ForeignKey("PurchaseId")]
    [InverseProperty("Invoices")]
    public virtual PurchaseEntity Purchase { get; set; } = null!;
}
