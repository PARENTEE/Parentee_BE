using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.RequestDTO.Users;
using Parentee_BE.DAL.Data.ResponseDTO.Users;

namespace Parentee_BE.DAL.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        // Request
        CreateMap<CreateUserRequestDTO, UserEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateUserRequestDTO, UserEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.UserFamilyRole, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        
        // Response
        CreateMap<UserEntity, GetUserResponseDTO>()
            .ForMember(dest => dest.Role, 
                opt => opt.MapFrom(src => src.UserFamilyRole != null 
                    ? src.UserFamilyRole.Role.ToString() 
                    : "None"));
    }
}