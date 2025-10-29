using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("child")]
[Index("FamilyId", Name = "idx_child_family")]
public partial class ChildEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("family_id")]
    public Guid FamilyId { get; set; }

    [Column("full_name")]
    public string FullName { get; set; } = null!;
    
    [Column("height")]
    [Precision(6, 2)]
    public decimal Height { get; set; }
    
    [Column("weight")]
    [Precision(6, 2)]
    public decimal Weight { get; set; }

    [Column("birth_date")]
    public DateOnly BirthDate { get; set; }

    [Column("gender")]
    public Gender? Gender { get; set; }

    [Column("photo_image_id")]
    public Guid? PhotoImageId { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Child")]
    public virtual ICollection<ChildPhotoEntity> ChildPhotos { get; set; } = new List<ChildPhotoEntity>();

    [InverseProperty("Child")]
    public virtual ICollection<ChildVaccinationEntity> ChildVaccinations { get; set; } = new List<ChildVaccinationEntity>();

    [InverseProperty("Child")]
    public virtual ICollection<DiaperChangeEntity> DiaperChanges { get; set; } = new List<DiaperChangeEntity>();

    [InverseProperty("Child")]
    public virtual ICollection<SolidFoodEntity> SolidFood { get; set; } = new List<SolidFoodEntity>();

    [ForeignKey("FamilyId")]
    [InverseProperty("Children")]
    public virtual FamilyEntity Family { get; set; } = null!;

    [InverseProperty("Child")]
    public virtual ICollection<FeedingEntity> Feedings { get; set; } = new List<FeedingEntity>();

    [InverseProperty("Child")]
    public virtual ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();

    [ForeignKey("PhotoImageId")]
    [InverseProperty("Children")]
    public virtual ImageEntity? PhotoImage { get; set; }

    [InverseProperty("Child")]
    public virtual ICollection<SleepEntity> Sleeps { get; set; } = new List<SleepEntity>();

    [InverseProperty("Child")]
    public virtual ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}
