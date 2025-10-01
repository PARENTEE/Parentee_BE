using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Users;

namespace Parentee_BE.DAL.Mappers;

public class UserRoleMapper : Profile
{
    public UserRoleMapper()
    {
        CreateMap<UserFamilyRoleEntity, UserRoleResponse>();
    }
    
}