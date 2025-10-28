using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO;
using Parentee_BE.DAL.Data.RequestDTO.Feedings;
using Parentee_BE.DAL.Data.ResponseDTO.Feedings;

namespace Parentee_BE.BLL.Services.Implements;

public class FeedingService(
    IUnitOfWork<AppDbContext> unitOfWork,
    ILogger<FeedingService> logger,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : BaseService<FeedingService>(unitOfWork, logger, httpContextAccessor), IFeedingService
{
    public async Task<GetFeedingResponse> GetFeedingById(Guid id)
    {
        var response = await unitOfWork.GetRepository<FeedingEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        return mapper.Map<GetFeedingResponse>(response);
    }

    public async Task<GetFeedingResponse> CreateFeeding(CreateFeedingRequest requestDto)
    {
        var feedingEntity = mapper.Map<FeedingEntity>(requestDto);
        Console.Write(feedingEntity);
        await unitOfWork.GetRepository<FeedingEntity>().InsertAsync(feedingEntity);
        return mapper.Map<FeedingEntity, GetFeedingResponse>(feedingEntity);;
    }

    public async Task<GetFeedingResponse> UpdateFeeding(Guid id, UpdateFeedingRequest requestDto)
    {
        // Check if User exists
        var feedingEntity = await unitOfWork.GetRepository<FeedingEntity>().FirstOrDefaultAsync(predicate: a => a.Id == id);
        if (feedingEntity == null) throw new NotFoundException("Feeding not found!");
        
        mapper.Map(requestDto, feedingEntity);
        unitOfWork.GetRepository<FeedingEntity>().UpdateAsync(feedingEntity);
        
        return mapper.Map<FeedingEntity, GetFeedingResponse>(feedingEntity);
    }

    public async Task<bool> DeleteFeeding(Guid id)
    {
        var feedingEntity = await unitOfWork.GetRepository<FeedingEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id);
        if (feedingEntity == null) throw new NotFoundException("Feeding not found!");
        
        // Delete User
        unitOfWork.GetRepository<FeedingEntity>().Delete(feedingEntity);
        
        return true;
    }
}