using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.RequestDTO.Children;

public class CreateChildRequestDTO
{
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string? Notes { get; set; }
}