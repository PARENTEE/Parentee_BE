using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.SolidFood;
using Parentee_BE.DAL.Data.ResponseDTO.SolidFood;

namespace Parentee_BE.BLL.Services.Implements;

public class SolidFoodService (
    IUnitOfWork<AppDbContext> unitOfWork,
    ILogger<SolidFoodService> logger,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : BaseService<SolidFoodService>(unitOfWork, logger, httpContextAccessor), ISolidFoodService
{

    public async Task<GetSolidFoodResponse> CreateSolidFood(CreateSolidFoodRequest requestDto)
    {
        var solidFoodEntity = mapper.Map<SolidFoodEntity>(requestDto);
        await unitOfWork.GetRepository<SolidFoodEntity>().InsertAsync(solidFoodEntity);
        return mapper.Map<SolidFoodEntity, GetSolidFoodResponse>(solidFoodEntity);;
    }
}