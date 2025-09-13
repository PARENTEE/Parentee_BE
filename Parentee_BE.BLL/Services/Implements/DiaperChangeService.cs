using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.DiaperChange;
using Parentee_BE.DAL.Data.ResponseDTO.DiaperChange;

namespace Parentee_BE.BLL.Services.Implements;

public class DiaperChangeService(IUnitOfWork <AppDbContext> unitOfWork,
    ILogger<DiaperChangeService> logger,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : BaseService<DiaperChangeService>(unitOfWork, logger, httpContextAccessor), IDiaperChangeService
{
    public async Task<CreateDiaperChangeResponse> CreateDiaperChange(CreateDiaperChangeRequest request)
    {
       var diaperRepository = unitOfWork.GetRepository<DiaperChangeEntity>();
       try
       {
           var diaperEntity = mapper.Map<DiaperChangeEntity>(request);
           await diaperRepository.InsertAsync(diaperEntity);
           await unitOfWork.SaveChangesAsync();
           var response = mapper.Map<CreateDiaperChangeResponse>(diaperEntity);
           return response;
       }
       catch (Exception e)
       {
           logger.LogError(e.Message);
           return null;
       }
    }

    public async Task<UpdateDiaperChangeResponse?> UpdateDiaperChange(Guid childId, UpdateDiaperChangeRequest request)
    {
        var diaperRepository = unitOfWork.GetRepository<DiaperChangeEntity>();
        var oldEntity = await diaperRepository.FirstOrDefaultAsync(predicate: x => x.ChildId == childId);
        if (oldEntity == null) return null;
       oldEntity.Type = request.Type;
       oldEntity.UpdatedAt = DateTime.UtcNow;
       oldEntity.Notes = request.Notes;
       oldEntity.RashObserved = request.RashObserved;
       
       diaperRepository.UpdateAsync(oldEntity);
       await unitOfWork.SaveChangesAsync();
       return mapper.Map<UpdateDiaperChangeResponse>(oldEntity);
    }

    public async Task<CreateDiaperChangeResponse> GetChildIdByDiaperChanges(Guid childId)
    {
        var diaperRepository = unitOfWork.GetRepository<DiaperChangeEntity>();
        var entity = await diaperRepository.FirstOrDefaultAsync(predicate: x => x.ChildId == childId);
        return entity == null ? null : mapper.Map<CreateDiaperChangeResponse>(entity);
    }
}