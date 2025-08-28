using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Keyless]
public partial class CalendarEventEntity
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid? Id { get; set; }

    [Column("family_id")]
    public Guid? FamilyId { get; set; }

    [Column("child_id")]
    public Guid? ChildId { get; set; }

    [Column("event_type")]
    public string? EventType { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("event_start")]
    public DateTime? EventStart { get; set; }

    [Column("event_end")]
    public DateTime? EventEnd { get; set; }

    [Column("all_day")]
    public bool? AllDay { get; set; }
}
