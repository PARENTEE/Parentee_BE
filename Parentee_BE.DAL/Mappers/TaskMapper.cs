using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Task;
using Parentee_BE.DAL.Data.ResponseDTO.Task;
using Parentee_BE.DAL.Data.ResponseDTO.Users;

namespace Parentee_BE.DAL.Mappers;

public class TaskMapper : Profile
{
    public TaskMapper()
    {
        // Response
        CreateMap<UserEntity, GetUserResponseDTO>()
            .ForMember(dest => dest.Role, 
                opt => opt.MapFrom(src => src.UserFamilyRole != null 
                    ? src.UserFamilyRole.Role.ToString() 
                    : "None"));
        // Request -> Entity
        CreateMap<CreateTaskRequest, TaskEntity>()
            .ForMember(dest => dest.StartsAt, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartsAt, DateTimeKind.Utc)))
            .ForMember(dest => dest.EndsAt, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EndsAt, DateTimeKind.Utc)));
        CreateMap<UpdateTaskRequest, TaskEntity>();

        // Entity -> Response
        CreateMap<TaskEntity, GetTaskResponse>();
    }
}