using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
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
    public async Task<CreateChildResponseDTO> CreateChild(CreateChildRequestDTO request)
    {
        var childRepository = unitOfWork.GetRepository<ChildEntity>();
        try
        {
            var childEntity = mapper.Map<ChildEntity>(request);
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

    public async Task<CreateChildResponseDTO> UpdateChild(Guid id, CreateChildRequestDTO request)
    {
        var childRepository = unitOfWork.GetRepository<ChildEntity>();
        var child = await childRepository.FirstOrDefaultAsync(predicate:c => c.Id == id);
        if (child == null)
        {
            logger.LogError($"Child with id {id} not found");
        }
        // mapper.Map(request, child);
        child.FullName = request.FullName;
        child.BirthDate = request.BirthDate;
        child.Sex = request.Sex;
        child.Notes = request.Notes;
        child.FamilyId = request.FamilyId;
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