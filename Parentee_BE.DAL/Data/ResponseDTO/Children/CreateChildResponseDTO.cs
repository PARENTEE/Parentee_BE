namespace Parentee_BE.DAL.Data.ResponseDTO.Children;

public class CreateChildResponseDTO
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Sex { get; set; }
    public Guid? PhotoImageId { get; set; }
    public string? Notes { get; set; }
}