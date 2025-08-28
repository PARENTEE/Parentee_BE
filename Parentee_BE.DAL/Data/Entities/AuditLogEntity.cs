using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("audit_log")]
public partial class AuditLogEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("family_id")]
    public Guid? FamilyId { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }

    [Column("action")]
    public string Action { get; set; } = null!;

    [Column("entity_type")]
    public string EntityType { get; set; } = null!;

    [Column("entity_id")]
    public Guid? EntityId { get; set; }

    [Column("detail", TypeName = "jsonb")]
    public string? Detail { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("FamilyId")]
    [InverseProperty("AuditLogs")]
    public virtual FamilyEntity? Family { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AuditLogs")]
    public virtual UserEntity? User { get; set; }
}
