using System.ComponentModel.DataAnnotations;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.RequestDTO.SolidFood;

public class BaseSolidFoodRequest
{
    [Required(ErrorMessage = "Child Id is required.")]
    public Guid ChildId { get; set; }

    [Required(ErrorMessage = "Ate time is required.")]
    public DateTime AteAt { get; set; }
    
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Quantity is required.")]
    [Range(0, 1000000, ErrorMessage = "Quantity must be between 0 and 1000000.")]
    public double Quantity { get; set; }
    
    [Required(ErrorMessage = "Food Unit is required.")]
    public FoodUnit Unit { get; set; }
    
    [Required(ErrorMessage = "Quantity is required.")]
    [MaxLength(1000, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string? Notes { get; set; }
}