using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDto.Auth;

public class LoginRequestDTO
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}