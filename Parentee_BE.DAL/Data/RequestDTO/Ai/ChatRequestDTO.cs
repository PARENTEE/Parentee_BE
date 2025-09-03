using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Ai;

public class ChatRequestDTO
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50, ErrorMessage = "Role length cannot be more than 256 characters")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Message is required")]
    [MaxLength(256, ErrorMessage = "Role length cannot be more than 256 characters")]
    public string Message { get; set; }
}