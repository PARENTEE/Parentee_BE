using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Sleep;

public class BaseSleepRequest
{
    [Required(ErrorMessage = "Child Id is required.")]
    public Guid ChildId { get; set; }
    
    [Required(ErrorMessage = "Start time is required.")]
    public DateTime StartTime { get; set; }
    
    [Required(ErrorMessage = "FamilyId is required.")]
    public DateTime EndTime { get; set; }

    [MaxLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
    public string? Location { get; set; }

    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string? Notes { get; set; }
}