using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Family;
using Parentee_BE.DAL.Data.ResponseDTO.Family;
using Parentee_BE.DAL.Data.ResponseDTO.Feedings;

namespace Parentee_BE.DAL.Mappers;

public class FamilyMapper : Profile
{
    public FamilyMapper()
    {
        CreateMap<UserFamilyRoleRequest, UserFamilyRoleEntity>();
        CreateMap<UserFamilyRoleEntity, UserFamilyRoleResponse>();
        CreateMap<UserFamilyRoleEntity, GetUserFamily>()
            .ForMember(dest => dest.FamilyRole, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.InvitationStatus,
                opt => opt.MapFrom(src => src.InvitationStatus))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.User.UpdatedAt));

        CreateMap<UserEntity, GetUserFamily>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FamilyRole, opt => opt.MapFrom(u => u.UserFamilyRole.Role));
        
        CreateMap<UserFamilyRoleEntity, GetUserFamily>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
            .ForMember(dest => dest.InvitationStatus, opt => opt.MapFrom(src => src.InvitationStatus))
            .ForMember(dest => dest.FamilyRole, opt => opt.MapFrom(u => u.Role));

        // Request -> Entity
        CreateMap<CreateFamilyRequest, FamilyEntity>();
        CreateMap<UpdateFamilyRequest, FamilyEntity>();
        
        // Entity -> Response
        CreateMap<FamilyEntity, GetFamilyResponse>()
            .ForMember(dest => dest.UserFamilyRoleResponses, opt => opt.MapFrom(s => s.UserFamilyRoles));
        CreateMap<FamilyEntity, GetFamilyDetailResponse>()
            .ForMember(dest => dest.FamilyUsers, opt => opt.MapFrom(src => src.UserFamilyRoles));
        CreateMap<UserFamilyRoleEntity, GetInvitationResponse>()
            .ForMember(dest => dest.UserFamilyRoleId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.InviterName, opt => opt.MapFrom(src => src.Family.CreatedByNavigation!.FullName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
    }
}