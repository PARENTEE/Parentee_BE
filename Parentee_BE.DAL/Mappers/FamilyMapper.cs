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

        CreateMap<UserEntity, GetUserFamily>()
            .ForMember(dest => dest.FamilyRole, opt => opt.MapFrom(u => u.UserFamilyRole.Role));
        
        // Request -> Entity
        CreateMap<CreateFamilyRequest, FamilyEntity>()
            .ForMember(dest => dest.UserFamilyRoles, opt => opt.MapFrom(s => s.MemberRoles));
        CreateMap<UpdateFamilyRequest, FamilyEntity>();
        
        // Entity -> Response
        CreateMap<FamilyEntity, GetFamilyResponse>()
            .ForMember(dest => dest.UserFamilyRoleResponses, opt => opt.MapFrom(s => s.UserFamilyRoles));
        CreateMap<FamilyEntity, GetFamilyDetailResponse>()
            .ForMember(dest => dest.FamilyUsers, opt => opt.MapFrom(s => s.UserFamilyRoles.Select(ufr => ufr.User)));
    }
}