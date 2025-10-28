using Parentee_BE.DAL.Data.RequestDTO.SolidFood;
using Parentee_BE.DAL.Data.ResponseDTO.SolidFood;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface ISolidFoodService
{
    Task<GetSolidFoodResponse> CreateSolidFood(CreateSolidFoodRequest request);

}