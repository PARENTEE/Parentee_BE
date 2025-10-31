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
        CreateMap<CreateTaskRequest, TaskEntity>()
            .ForMember(dest => dest.StartsAt, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartsAt, DateTimeKind.Utc)))
            .ForMember(dest => dest.EndsAt, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EndsAt, DateTimeKind.Utc)));
        CreateMap<UpdateTaskRequest, TaskEntity>();

        // Entity -> Response
        CreateMap<TaskEntity, GetTaskResponse>();
    }
}