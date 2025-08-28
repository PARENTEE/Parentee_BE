using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("notification_outbox")]
public partial class NotificationOutboxEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("family_id")]
    public Guid FamilyId { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }
    
    [Column("channel")]
    public ReminderChannel Channel { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("body")]
    public string? Body { get; set; }

    [Column("scheduled_at")]
    public DateTime ScheduledAt { get; set; }

    [Column("sent_at")]
    public DateTime? SentAt { get; set; }

    [Column("attempts")]
    public int Attempts { get; set; }

    [Column("payload", TypeName = "jsonb")]
    public string? Payload { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("FamilyId")]
    [InverseProperty("NotificationOutboxes")]
    public virtual FamilyEntity Family { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("NotificationOutboxes")]
    public virtual UserEntity? User { get; set; }
}
