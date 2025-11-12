using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Ai;

public class ChatRequestDTO
{
    [Required(ErrorMessage = "Message is required")]
    [MaxLength(256, ErrorMessage = "Role length cannot be more than 256 characters")]
    public string Message { get; set; }
}