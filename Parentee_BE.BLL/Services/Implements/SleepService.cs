using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.RequestDTO.Sleep;
using Parentee_BE.DAL.Data.ResponseDTO.Sleep;
using Parentee_BE.DAL.Data.Repositories.Interfaces;

namespace Parentee_BE.BLL.Services.Implements;

public class SleepService(
    IUnitOfWork<AppDbContext> unitOfWork,
    ILogger<SleepService> logger,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper
) : BaseService<SleepService>(unitOfWork, logger, httpContextAccessor), ISleepService
{
    public async Task<GetSleepResponse> GetSleepById(Guid id)
    {
        var entity = await unitOfWork.GetRepository<SleepEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        if (entity == null) throw new NotFoundException("Sleep record not found!");
        return mapper.Map<GetSleepResponse>(entity);
    }

    public async Task<GetSleepResponse> CreateSleep(CreateSleepRequest requestDto)
    {
        var entity = mapper.Map<CreateSleepRequest, SleepEntity>(requestDto);
        await unitOfWork.GetRepository<SleepEntity>().InsertAsync(entity);
        return mapper.Map<SleepEntity, GetSleepResponse>(entity);
    }

    public async Task<GetSleepResponse> UpdateSleep(Guid id, UpdateSleepRequest requestDto)
    {
        var entity = await unitOfWork.GetRepository<SleepEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        if (entity == null) throw new NotFoundException("Sleep record not found!");

        mapper.Map(requestDto, entity);
        unitOfWork.GetRepository<SleepEntity>().UpdateAsync(entity);

        return mapper.Map<SleepEntity, GetSleepResponse>(entity);
    }

    public async Task<bool> DeleteSleep(Guid id)
    {
        var entity = await unitOfWork.GetRepository<SleepEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        if (entity == null) throw new NotFoundException("Sleep record not found!");

        unitOfWork.GetRepository<SleepEntity>().Delete(entity);
        return true;
    }
}