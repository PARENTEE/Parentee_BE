namespace Parentee_BE.DAL.Data.ResponseDTO.Accounts;

public class GetAccountResponseDTO
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string Role { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}