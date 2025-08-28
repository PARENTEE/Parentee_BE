using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("child_photo")]
public partial class ChildPhotoEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("child_id")]
    public Guid ChildId { get; set; }

    [Column("image_id")]
    public Guid ImageId { get; set; }

    [Column("taken_at")]
    public DateTime? TakenAt { get; set; }

    [Column("caption")]
    public string? Caption { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("ChildId")]
    [InverseProperty("ChildPhotos")]
    public virtual ChildEntity Child { get; set; } = null!;

    [ForeignKey("ImageId")]
    [InverseProperty("ChildPhotos")]
    public virtual ImageEntity Image { get; set; } = null!;
}
