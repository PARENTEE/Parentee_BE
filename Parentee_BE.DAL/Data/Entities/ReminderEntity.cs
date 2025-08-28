using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("reminder")]
public partial class ReminderEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("task_id")]
    public Guid TaskId { get; set; }

    [Column("remind_at")]
    public DateTime RemindAt { get; set; }
    
    [Column("channel")]
    public ReminderChannel Channel { get; set; }

    [Column("payload", TypeName = "jsonb")]
    public string? Payload { get; set; }

    [Column("sent_at")]
    public DateTime? SentAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("Reminders")]
    public virtual TaskEntity Task { get; set; } = null!;
}
