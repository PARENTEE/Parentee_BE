namespace Parentee_BE.DAL.Data.ResponseDTO.Family;

public class GetInvitationResponse
{
    public Guid UserFamilyRoleId { get; set; }
    public string InviterName { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}