using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("vaccine_catalog")]
[Index("Code", Name = "vaccine_catalog_code_key", IsUnique = true)]
public partial class VaccineCatalogEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("code")]
    public string? Code { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("recommended_age_days")]
    public int? RecommendedAgeDays { get; set; }

    [Column("doses")]
    public int? Doses { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("Vaccine")]
    public virtual ICollection<ChildVaccinationEntity> ChildVaccinations { get; set; } = new List<ChildVaccinationEntity>();
}
