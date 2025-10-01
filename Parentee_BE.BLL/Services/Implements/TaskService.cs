using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.RequestDTO.Task;
using Parentee_BE.DAL.Data.ResponseDTO.Task;
using Parentee_BE.DAL.Data.Repositories.Interfaces;

namespace Parentee_BE.BLL.Services.Implements;

public class TaskService(
    IUnitOfWork<AppDbContext> unitOfWork,
    ILogger<TaskService> logger,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper
) : BaseService<TaskService>(unitOfWork, logger, httpContextAccessor), ITaskService
{
    public async Task<GetTaskResponse> GetTaskById(Guid id)
    {
        var entity = await unitOfWork.GetRepository<TaskEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        if (entity == null) throw new NotFoundException("Task not found!");
        return mapper.Map<GetTaskResponse>(entity);
    }

    public async Task<GetTaskResponse> CreateTask(CreateTaskRequest requestDto)
    {
        var entity = mapper.Map<CreateTaskRequest, TaskEntity>(requestDto);
        await unitOfWork.GetRepository<TaskEntity>().InsertAsync(entity);
        return mapper.Map<TaskEntity, GetTaskResponse>(entity);
    }

    public async Task<GetTaskResponse> UpdateTask(Guid id, UpdateTaskRequest requestDto)
    {
        var entity = await unitOfWork.GetRepository<TaskEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        if (entity == null) throw new NotFoundException("Task not found!");

        mapper.Map(requestDto, entity);
        unitOfWork.GetRepository<TaskEntity>().UpdateAsync(entity);

        return mapper.Map<TaskEntity, GetTaskResponse>(entity);
    }

    public async Task<bool> DeleteTask(Guid id)
    {
        var entity = await unitOfWork.GetRepository<TaskEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );
        if (entity == null) throw new NotFoundException("Task not found!");

        unitOfWork.GetRepository<TaskEntity>().Delete(entity);
        return true;
    }
}