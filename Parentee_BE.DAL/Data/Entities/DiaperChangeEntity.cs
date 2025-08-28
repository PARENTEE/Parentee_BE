using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("diaper_change")]
[Index("ChildId", "ChangedAt", Name = "idx_diaper_child_time", IsDescending = new[] { false, true })]
public partial class DiaperChangeEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("family_id")]
    public Guid FamilyId { get; set; }

    [Column("child_id")]
    public Guid ChildId { get; set; }

    [Column("changed_at")]
    public DateTime ChangedAt { get; set; }
    
    [Column("type")]
    public DiaperType Type { get; set; }

    [Column("rash_observed")]
    public bool? RashObserved { get; set; }

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
    [InverseProperty("DiaperChanges")]
    public virtual ChildEntity Child { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("DiaperChanges")]
    public virtual UserEntity? CreatedByNavigation { get; set; }

    [ForeignKey("FamilyId")]
    [InverseProperty("DiaperChanges")]
    public virtual FamilyEntity Family { get; set; } = null!;
}
