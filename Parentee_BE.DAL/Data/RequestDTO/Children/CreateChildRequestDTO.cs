namespace Parentee_BE.DAL.Data.RequestDTO.Children;

public class CreateChildRequestDTO
{
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Sex { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string? Notes { get; set; }
}