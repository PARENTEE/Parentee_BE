using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Sleep;

public class BaseSleepRequest
{
    [Required(ErrorMessage = "Start time is required.")]
    public DateTime StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public int? DurationMin { get; set; }

    [MaxLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
    public string? Location { get; set; }

    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string? Notes { get; set; }
}