using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.DiaperChange;
using Parentee_BE.DAL.Data.ResponseDTO.DiaperChange;

namespace Parentee_BE.BLL.Services.Implements;

public class DiaperChangeService(
    IUnitOfWork<AppDbContext> unitOfWork,
    ILogger<DiaperChangeService> logger,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper
) : BaseService<DiaperChangeService>(unitOfWork, logger, httpContextAccessor), IDiaperChangeService
{
    public async Task<GetDiaperChangeResponse> GetDiaperChangeById(Guid id)
    {
        var entity = await unitOfWork.GetRepository<DiaperChangeEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        if (entity == null) throw new NotFoundException("Diaper change not found!");
        return mapper.Map<GetDiaperChangeResponse>(entity);
    }

    public async Task<GetDiaperChangeResponse> CreateDiaperChange(CreateDiaperChangeRequest requestDto)
    {
        var entity = mapper.Map<CreateDiaperChangeRequest, DiaperChangeEntity>(requestDto);
        await unitOfWork.GetRepository<DiaperChangeEntity>().InsertAsync(entity);
        return mapper.Map<DiaperChangeEntity, GetDiaperChangeResponse>(entity);
    }

    public async Task<GetDiaperChangeResponse> UpdateDiaperChange(Guid id, UpdateDiaperChangeRequest requestDto)
    {
        var entity = await unitOfWork.GetRepository<DiaperChangeEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        if (entity == null) throw new NotFoundException("Diaper change not found!");

        mapper.Map(requestDto, entity);
        unitOfWork.GetRepository<DiaperChangeEntity>().UpdateAsync(entity);

        return mapper.Map<DiaperChangeEntity, GetDiaperChangeResponse>(entity);
    }

    public async Task<bool> DeleteDiaperChange(Guid id)
    {
        var entity = await unitOfWork.GetRepository<DiaperChangeEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        if (entity == null) throw new NotFoundException("Diaper change not found!");

        unitOfWork.GetRepository<DiaperChangeEntity>().Delete(entity);
        return true;
    }
}