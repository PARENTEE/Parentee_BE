using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.ResponseDTO.SolidFood;

public class GetSolidFoodResponse
{
    public Guid Id { get; set; }

    public Guid ChildId { get; set; }

    public DateTime AteAt { get; set; }

    public string Name { get; set; }
    
    public double Quantity { get; set; }
    
    public FoodUnit Unit { get; set; }
    
    public string? Notes { get; set; }
}