using System.ComponentModel.DataAnnotations;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.RequestDTO.Feedings;

public class BaseFeedingRequest
{
    [Required(ErrorMessage = "FamilyId is required.")]
    public Guid FamilyId { get; set; }

    [Required(ErrorMessage = "ChildId is required.")]
    public Guid ChildId { get; set; }

    [Required(ErrorMessage = "Feeding method is required.")]
    public FeedingMethod Method { get; set; }

    [Required(ErrorMessage = "Start time is required.")]
    public DateTime StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0.")]
    public int? DurationMin { get; set; }

    [Range(0.1, 9999.9, ErrorMessage = "Amount must be between 0.1 and 9999.9 ml.")]
    public decimal? AmountMl { get; set; }

    [MaxLength(10, ErrorMessage = "Side value cannot exceed 10 characters.")]
    public string? Side { get; set; }

    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string? Notes { get; set; }
}