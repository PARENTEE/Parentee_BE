using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("task_recurrence")]
public partial class TaskRecurrenceEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("task_id")]
    public Guid TaskId { get; set; }

    [Column("rule")]
    public string Rule { get; set; } = null!;

    [Column("timezone")]
    public string? Timezone { get; set; }

    [Column("until")]
    public DateTime? Until { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskRecurrences")]
    public virtual TaskEntity Task { get; set; } = null!;
}
