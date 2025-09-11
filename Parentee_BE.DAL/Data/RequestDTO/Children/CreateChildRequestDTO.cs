namespace Parentee_BE.DAL.Data.RequestDTO.Children;

public class CreateChildRequestDTO
{
    // public Guid ChildId { get; set; }
    public Guid FamilyId { get; set; }
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Sex { get; set; }
    // public Guid? PhotoImageId { get; set; }
    public string? Notes { get; set; }
}