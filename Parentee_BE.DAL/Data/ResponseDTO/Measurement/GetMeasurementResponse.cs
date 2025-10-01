using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.ResponseDTO.Measurement;
public class GetMeasurementResponse
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public Guid ChildId { get; set; }
    public MeasureType Type { get; set; }
    public DateTime MeasuredAt { get; set; }
    public decimal Value { get; set; }
    public string Unit { get; set; } = null!;
    public string? Source { get; set; }
    public string? Notes { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}