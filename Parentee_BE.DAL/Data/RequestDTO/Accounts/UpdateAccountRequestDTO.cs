using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.ResponseDTO.Accounts;

public class UpdateAccountRequestDTO
{
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(50, ErrorMessage = "First name length cannot be more than 50 characters")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(50, ErrorMessage = "Last name length cannot be more than 50 characters")]
    public string LastName { get; set; }
}