using System.ComponentModel.DataAnnotations;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.RequestDTO.Feedings;

public class BaseFeedingRequest
{
    [Required(ErrorMessage = "ChildId is required.")]
    public Guid ChildId { get; set; }

    [Required(ErrorMessage = "Feeding method is required.")]
    public FeedingMethod Method { get; set; }

    [Required(ErrorMessage = "Start time is required.")]
    public DateTime StartedAt { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Left duration must be greater than 0.")]
    public TimeSpan LeftDuration { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Right duration must be greater than 0.")]
    public TimeSpan RightDuration { get; set; }

    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string? Notes { get; set; }
}