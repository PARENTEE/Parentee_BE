using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        
        // Request -> Entity
        CreateMap<CreateFamilyRequest, FamilyEntity>()
            .ForMember(dest => dest.UserFamilyRoles, opt => opt.MapFrom(s => s.MemberRoles));
        CreateMap<UpdateFamilyRequest, FamilyEntity>();

        // Entity -> Response
        CreateMap<FamilyEntity, GetFamilyResponse>()
            .ForMember(dest => dest.UserFamilyRoleResponses, opt => opt.MapFrom(s => s.UserFamilyRoles));
    }
}