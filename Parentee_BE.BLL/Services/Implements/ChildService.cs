using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.Children;
using Parentee_BE.DAL.Data.ResponseDTO.Children;

namespace Parentee_BE.BLL.Services.Implements;

public class ChildService(
    IUnitOfWork<AppDbContext> unitOfWork,
    ILogger<ChildService> logger,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : BaseService<ChildService>(unitOfWork, logger, httpContextAccessor), IChildService
{
    [Authorize]
    public async Task<CreateChildResponseDTO> CreateChild(CreateChildRequestDTO request)
    {
        var userId = GetCurrentAccountIdThroughToken();
        
        // Find family in family roles
        var userFamilyRoleEntity = await unitOfWork.GetRepository<UserFamilyRoleEntity>()
            .FirstOrDefaultAsync(predicate: u => u.UserId == userId);
        if (userFamilyRoleEntity == null)
            throw new BusinessException("Người dùng không nằm trong gia đình nào cả!");

        var childRepository = unitOfWork.GetRepository<ChildEntity>();
        try
        {
            var childEntity = mapper.Map<ChildEntity>(request);
            childEntity.FamilyId = userFamilyRoleEntity.FamilyId;
            
            await childRepository.InsertAsync(childEntity);
            await unitOfWork.SaveChangesAsync();
            var response = mapper.Map<CreateChildResponseDTO>(childEntity);
            return response;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return null;
        }
    }

    [Authorize]
    public async Task<IEnumerable<CreateChildResponseDTO>> GetChildrenInCurrentFamily()
    {
        var userId = GetCurrentAccountIdThroughToken();
        
        // Find family in family roles
        var userFamilyRoleEntity = await unitOfWork.GetRepository<UserFamilyRoleEntity>()
            .FirstOrDefaultAsync(predicate: u => u.UserId == userId);
        if (userFamilyRoleEntity == null)
            throw new BusinessException("Người dùng không nằm trong gia đình nào cả!");
        
        var childRepository = unitOfWork.GetRepository<ChildEntity>();
        var children = await childRepository.GetListAsync(predicate: c => c.FamilyId == userFamilyRoleEntity.FamilyId);
        // return children;
        
        return mapper.Map<IEnumerable<CreateChildResponseDTO>>(children);
    }
    public async Task<IEnumerable<CreateChildResponseDTO>> GetAllChildren()
    {
        var childRepository = unitOfWork.GetRepository<ChildEntity>();
        var children = await childRepository.GetListAsync();
        // return children;
        
        return mapper.Map<IEnumerable<CreateChildResponseDTO>>(children);
    }

    public async Task<CreateChildResponseDTO> GetChildById(Guid id)
    {
        var childRepository = unitOfWork.GetRepository<ChildEntity>();
        var child = await childRepository.FirstOrDefaultAsync(predicate: c => c.Id == id);

        if (child == null)
        {
            logger.LogError($"Child with id {id} not found");
            return null;
        }
        return mapper.Map<CreateChildResponseDTO>(child);
    }
    
    public async Task<ChildEntity> GetChildTodayById(Guid id)
    {
        var today = DateTime.UtcNow.Date;
        var childRepository = unitOfWork.GetRepository<ChildEntity>();
        var child = await childRepository.FirstOrDefaultAsync(predicate: c => c.Id == id,
            include: q => q
                .Include(c => c.Measurements.OrderByDescending(m => m.CreatedAt).Take(1))
                .Include(c => c.DiaperChanges.Where(dp => dp.CreatedAt.Date == today))
                .Include(c => c.Feedings.Where(f => f.CreatedAt.Date == today))
                .Include(c => c.Sleeps.Where(s => s.CreatedAt.Date == today)));

        return child == null ? 
            throw new NotFoundException($"Child with id {id} not found") : 
            child;
    }

    public async Task<CreateChildResponseDTO> UpdateChild(Guid id, CreateChildRequestDTO request)
    {
        var childRepository = unitOfWork.GetRepository<ChildEntity>();
        var child = await childRepository.FirstOrDefaultAsync(predicate:c => c.Id == id);
        if (child == null)
        {
            logger.LogError($"Child with id {id} not found");
            throw new NotFoundException($"Child with id {id} not found");
        }
        // mapper.Map(request, child);
        child.FullName = request.FullName;
        child.BirthDate = request.BirthDate;
        child.Sex = request.Sex;
        child.Notes = request.Notes;
        child.UpdatedAt = DateTime.UtcNow;
        childRepository.UpdateAsync(child);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<CreateChildResponseDTO>(child);
    }

    public async Task<bool> DeleteChild(Guid id)
    {
        var childRepository = unitOfWork.GetRepository<ChildEntity>();
        var child = await childRepository.FirstOrDefaultAsync(predicate:c => c.Id == id);

        if (child == null)
        {
            return false;
        }
        childRepository.Delete(child);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}