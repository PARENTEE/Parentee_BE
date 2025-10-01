using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Task;
using Parentee_BE.DAL.Data.ResponseDTO.Task;

namespace Parentee_BE.DAL.Mappers;

public class TaskMapper : Profile
{
    public TaskMapper()
    {
        // Request -> Entity
        CreateMap<CreateTaskRequest, TaskEntity>();
        CreateMap<UpdateTaskRequest, TaskEntity>();

        // Entity -> Response
        CreateMap<TaskEntity, GetTaskResponse>();
    }
}