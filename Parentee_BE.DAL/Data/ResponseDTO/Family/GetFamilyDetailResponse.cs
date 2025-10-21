using Parentee_BE.DAL.Data.ResponseDTO.Users;

namespace Parentee_BE.DAL.Data.ResponseDTO.Family;

public class GetFamilyDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid? CoverImageId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<GetUserFamily> FamilyUsers { get; set; } = null!;
}