using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Family;

public class CreateFamilyRequest : BaseFamilyRequest
{
    [Required(ErrorMessage = "MemberRole is required.")]
    public List<UserFamilyRoleRequest> MemberRoles { get; set; } = new();
}