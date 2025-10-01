using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.RequestDTO.Users;

public class UserRoleResponse
{
    public Guid UserId { get; set; }
    public FamilyRole RoleId { get; set; }
    public Guid FamilyId { get; set; }
    
}