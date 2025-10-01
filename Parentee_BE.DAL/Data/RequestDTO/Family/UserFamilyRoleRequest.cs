using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.RequestDTO.Family;

public class UserFamilyRoleRequest
{
    public Guid UserId { get; set; }
    public FamilyRole Role { get; set; }
}