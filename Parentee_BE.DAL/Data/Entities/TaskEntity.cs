using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Parentee_BE.DAL.Data.Enums.TaskStatus;

namespace Parentee_BE.DAL.Data.Entities;

[Table("task")]
public partial class TaskEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("child_id")]
    public Guid? ChildId { get; set; }

    [Column("title")]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("starts_at")]
    public DateTime? StartsAt { get; set; }

    [Column("ends_at")]
    public DateTime? EndsAt { get; set; }

    [Column("all_day")]
    public bool AllDay { get; set; }
    
    [Column("status")]
    public TaskStatus Status { get; set; }
    
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
    [InverseProperty("Tasks")]
    public virtual ChildEntity? Child { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("TaskCreatedByNavigations")]
    public virtual UserEntity? CreatedByNavigation { get; set; }

    [InverseProperty("Task")]
    public virtual ICollection<ReminderEntity> Reminders { get; set; } = new List<ReminderEntity>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskRecurrenceEntity> TaskRecurrences { get; set; } = new List<TaskRecurrenceEntity>();

    [ForeignKey("UpdatedBy")]
    [InverseProperty("TaskUpdatedByNavigations")]
    public virtual UserEntity? UpdatedByNavigation { get; set; }
}
