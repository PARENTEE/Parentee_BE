using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.ResponseDTO.Family;

public class GetUserFamily
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string Role { get; set; }
    
    public string FullName { get; set; }
    
    public Gender Gender { get; set; }
    
    public string FamilyRole { get; set; }
    
    public int InvitationStatus { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}