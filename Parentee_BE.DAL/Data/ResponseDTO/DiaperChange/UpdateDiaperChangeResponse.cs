using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.ResponseDTO.DiaperChange;

public class UpdateDiaperChangeResponse
{
    public Guid ChildId { get; set; }
    public Guid FamilyId { get; set; }
    public DateTime ChangedAt { get; set; }
    public bool? RashObserved { get; set; }
    public string? Notes { get; set; }
    public DiaperType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}