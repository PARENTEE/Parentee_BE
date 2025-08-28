using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("image")]
[Index("FamilyId", Name = "idx_image_family")]
public partial class ImageEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("family_id")]
    public Guid? FamilyId { get; set; }

    [Column("owner_user_id")]
    public Guid? OwnerUserId { get; set; }

    [Column("url")]
    public string Url { get; set; } = null!;

    [Column("mime_type")]
    public string? MimeType { get; set; }

    [Column("size_bytes")]
    public long? SizeBytes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Image")]
    public virtual ICollection<ChildPhotoEntity> ChildPhotos { get; set; } = new List<ChildPhotoEntity>();

    [InverseProperty("PhotoImage")]
    public virtual ICollection<ChildEntity> Children { get; set; } = new List<ChildEntity>();

    [InverseProperty("CoverImage")]
    public virtual ICollection<FamilyEntity> Families { get; set; } = new List<FamilyEntity>();

    [ForeignKey("FamilyId")]
    [InverseProperty("Images")]
    public virtual FamilyEntity? Family { get; set; }

    [InverseProperty("PdfImage")]
    public virtual ICollection<InvoiceEntity> Invoices { get; set; } = new List<InvoiceEntity>();

    [ForeignKey("OwnerUserId")]
    [InverseProperty("Images")]
    public virtual UserEntity? OwnerUser { get; set; }

    [InverseProperty("AvatarImage")]
    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}
