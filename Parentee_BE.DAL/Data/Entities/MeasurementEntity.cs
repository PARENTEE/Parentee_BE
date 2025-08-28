using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("measurement")]
[Index("ChildId", "MeasuredAt", Name = "idx_measurement_child_time", IsDescending = new[] { false, true })]
public partial class MeasurementEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("family_id")]
    public Guid FamilyId { get; set; }

    [Column("child_id")]
    public Guid ChildId { get; set; }
    
    [Column("type")]
    public MeasureType Type { get; set; }

    [Column("measured_at")]
    public DateTime MeasuredAt { get; set; }

    [Column("value")]
    [Precision(6, 2)]
    public decimal Value { get; set; }

    [Column("unit")]
    public string Unit { get; set; } = null!;

    [Column("source")]
    public string? Source { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("ChildId")]
    [InverseProperty("Measurements")]
    public virtual ChildEntity Child { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("Measurements")]
    public virtual UserEntity? CreatedByNavigation { get; set; }

    [ForeignKey("FamilyId")]
    [InverseProperty("Measurements")]
    public virtual FamilyEntity Family { get; set; } = null!;
}
