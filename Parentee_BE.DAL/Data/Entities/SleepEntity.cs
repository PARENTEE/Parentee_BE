using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("sleep")]
[Index("ChildId", "StartedAt", Name = "idx_sleep_child_time", IsDescending = new[] { false, true })]
public partial class SleepEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("family_id")]
    public Guid FamilyId { get; set; }

    [Column("child_id")]
    public Guid ChildId { get; set; }

    [Column("started_at")]
    public DateTime StartedAt { get; set; }

    [Column("ended_at")]
    public DateTime? EndedAt { get; set; }

    [Column("duration_min")]
    public int? DurationMin { get; set; }

    [Column("location")]
    public string? Location { get; set; }

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
    [InverseProperty("Sleeps")]
    public virtual ChildEntity Child { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("Sleeps")]
    public virtual UserEntity? CreatedByNavigation { get; set; }

    [ForeignKey("FamilyId")]
    [InverseProperty("Sleeps")]
    public virtual FamilyEntity Family { get; set; } = null!;
}
