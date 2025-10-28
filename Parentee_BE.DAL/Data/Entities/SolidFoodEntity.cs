using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("solid_food")]
[Index("ChildId", "AteAt", Name = "idx_diaper_child_time", IsDescending = new[] { false, true })]
public partial class SolidFoodEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("child_id")]
    public Guid ChildId { get; set; }

    [Column("ate_at")]
    public DateTime AteAt { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("quantity")]
    public double Quantity { get; set; }
    
    [Column("unit")]
    public FoodUnit Unit { get; set; }
    
    [Column("notes")]
    public string? Notes { get; set; }

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("ChildId")]
    [InverseProperty("SolidFood")]
    public virtual ChildEntity Child { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("SolidFood")]
    public virtual UserEntity? CreatedByNavigation { get; set; }
}