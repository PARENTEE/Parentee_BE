using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Users;

public class CreateUserRequestDTO
{
    [Required(ErrorMessage = "Email is required")]
    [MaxLength(100, ErrorMessage = "Email length cannot be more than 100 characters")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [Length(8, 100, ErrorMessage = "Password length must be in between 10 and 100 characters")]
    public string Password { get; set; }    
    
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(50, ErrorMessage = "First name length cannot be more than 50 characters")]
    public string FullName { get; set; }
    
    [Required(ErrorMessage = "Phone number is required")]
    [MaxLength(15, ErrorMessage = "Phone number cannot be more than 15 digits")]
    [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Invalid phone number format")]
    public string Phone { get; set; }
    
//    [Required(ErrorMessage = "Dob is required!")]
//    public DateOnly Dob { get; set; }
}