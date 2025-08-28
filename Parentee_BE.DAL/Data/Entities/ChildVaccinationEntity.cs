using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("child_vaccination")]
[Index("ChildId", "ScheduledAt", Name = "idx_child_vacc_sched")]
public partial class ChildVaccinationEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("family_id")]
    public Guid FamilyId { get; set; }

    [Column("child_id")]
    public Guid ChildId { get; set; }

    [Column("vaccine_id")]
    public Guid? VaccineId { get; set; }

    [Column("custom_name")]
    public string? CustomName { get; set; }

    [Column("scheduled_at")]
    public DateTime? ScheduledAt { get; set; }
    
    [Column("status")]
    public VaccinationStatus Status { get; set; }

    [Column("administered_at")]
    public DateTime? AdministeredAt { get; set; }

    [Column("lot_number")]
    public string? LotNumber { get; set; }

    [Column("provider_name")]
    public string? ProviderName { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("updated_by")]
    public Guid? UpdatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("ChildId")]
    [InverseProperty("ChildVaccinations")]
    public virtual ChildEntity Child { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("ChildVaccinationCreatedByNavigations")]
    public virtual UserEntity? CreatedByNavigation { get; set; }

    [ForeignKey("FamilyId")]
    [InverseProperty("ChildVaccinations")]
    public virtual FamilyEntity Family { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("ChildVaccinationUpdatedByNavigations")]
    public virtual UserEntity? UpdatedByNavigation { get; set; }

    [ForeignKey("VaccineId")]
    [InverseProperty("ChildVaccinations")]
    public virtual VaccineCatalogEntity? Vaccine { get; set; }
}
