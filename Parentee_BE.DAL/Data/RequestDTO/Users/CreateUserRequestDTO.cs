using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Users;

public class CreateUserRequestDTO
{
    [Required(ErrorMessage = "Email is required")]
    [MaxLength(100, ErrorMessage = "Email length cannot be more than 100 characters")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [Length(10, 100, ErrorMessage = "Password length must be in between 10 and 100 characters")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Role is required (Admin or User)")]
    [MaxLength(10, ErrorMessage = "Role length cannot be more than 10 characters")]
    public string Role { get; set; }
    
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(50, ErrorMessage = "First name length cannot be more than 50 characters")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(50, ErrorMessage = "Last name length cannot be more than 50 characters")]
    public string LastName { get; set; }
    
}