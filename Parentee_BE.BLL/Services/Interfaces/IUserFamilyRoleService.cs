using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Users;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IUserFamilyRoleService
{
    Task<UserRoleResponse> GetFamilyIdFromByUserID(Guid userId);
}