using Parentee_BE.DAL.Data.Entities;

namespace Parentee_BE.DAL.Data.ResponseDTO.Family;

public class GetFamilyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid? CoverImageId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public UserFamilyRoleEntity UserFamilyRoles  { get; set; } = null!;
}