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
using TaskStatus = Parentee_BE.DAL.Data.Enums.TaskStatus;

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

    public async Task<ICollection<GetTaskResponse>> GetTasksByFamilyIdAndDate(Guid childId, DateTime date)
    {
        // 1. Define the date range for the entire day
        var startDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        var endDate = startDate.AddDays(1); // The start of the next day

        var tasks = await unitOfWork.GetRepository<TaskEntity>()
            .GetListAsync<GetTaskResponse>(
            
                // 2. Correctly map properties from the entity 't'
                selector: t => new GetTaskResponse()
                {
                    Id = t.Id,
                    ChildId = t.ChildId,
                    Title = t.Title,
                    StartsAt = t.StartsAt, // <-- Use the actual start time from the task
                    EndsAt = t.EndsAt,     // <-- Use the actual end time from the task
                    Status = t.Status,
                    CreatedBy = t.CreatedBy,
                },

                // 3. Use the date range in the predicate
                predicate: t => // t.FamilyId == familyId && 
                                t.StartsAt >= startDate && 
                                t.StartsAt < endDate,
            
                // (Optional but recommended) Order the tasks by time
                orderBy: q => q.OrderBy(t => t.StartsAt)
            );

        // 4. Directly return the result. GetListAsync never returns null.
        return tasks;
    }
    // public async Task<ICollection<GetTaskResponse>> GetTaskByFamilyIdAndDate(Guid familyId, DateTime date)
    // {
    //     var entity = await unitOfWork.GetRepository<TaskEntity>()
    //         .GetListAsync<GetTaskResponse>(selector: t => 
    //                 new GetTaskResponse()
    //                 {
    //                     FamilyId = familyId,
    //                     StartsAt = date,
    //                     EndsAt = date,
    //                     ChildId = t.ChildId,
    //                     Title = t.Title,
    //                     Description = t.Description,
    //                     Status = t.Status.ToString(),
    //                     CreatedAt = t.CreatedAt,
    //                     UpdatedAt = t.UpdatedAt,
    //                     AllDay = t.AllDay,
    //                     CreatedBy = t.CreatedBy,
    //                     Id = t.Id,
    //                     DeletedAt = t.DeletedAt    
    //                 },
    //         predicate: a => a.FamilyId == familyId && a.StartsAt == date || 
    //                         a.FamilyId == familyId && a.EndsAt == date
    //     );
    //     // if (entity == null) throw new NotFoundException("Task not found!");
    //     if (entity == null) return new List<GetTaskResponse>();
    //     return entity;
    // }

    public async Task<GetTaskResponse> CreateTask(CreateTaskRequest requestDto)
    {
        var entity = mapper.Map<TaskEntity>(requestDto);
        await unitOfWork.GetRepository<TaskEntity>().InsertAsync(entity);
        return mapper.Map<TaskEntity, GetTaskResponse>(entity);
    }

    public async Task<GetTaskResponse> UpdateTask(Guid id, UpdateTaskRequest updateDto)
    {
        // Find the existing task
        var task = await unitOfWork.GetRepository<TaskEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id
        );

        if (task == null)
        {
            throw new NotFoundException("Task not found!");
        }

        // This manual mapping is fine, but AutoMapper is cleaner if you have it set up.
        // Let's uncomment your mapper code to make this cleaner.
        mapper.Map(updateDto, task); // This will map non-null values from the DTO to the entity

        // Update the entity in the repository
        unitOfWork.GetRepository<TaskEntity>().UpdateAsync(task);

        // Save the changes to the database
        await unitOfWork.SaveChangesAsync();

        // Map the updated entity back to a response object
        return mapper.Map<TaskEntity, GetTaskResponse>(task);
    }

    public async Task<int> UpdateTaskStatus(Guid id, TaskStatus taskStatus)
    {
        var entity = new TaskEntity() { Id = id, Status = taskStatus };

        unitOfWork.Context.Tasks.Attach(entity);
        unitOfWork.Context.Entry(entity).Property(e => e.Status).IsModified = true;

        return await unitOfWork.Context.SaveChangesAsync();
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