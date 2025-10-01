using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.ResponseDTO.Feedings;

public class GetFeedingResponse
{
    public Guid Id { get; set; }

    public Guid FamilyId { get; set; }

    public Guid ChildId { get; set; }

    public FeedingMethod Method { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public int? DurationMin { get; set; }

    public decimal? AmountMl { get; set; }

    public string? Side { get; set; }

    public string? Notes { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}