namespace Parentee_BE.DAL.Data.ResponseDTO.Users;

public class GetUserResponseDTO
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string Role { get; set; }
    
    public string FullName { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}