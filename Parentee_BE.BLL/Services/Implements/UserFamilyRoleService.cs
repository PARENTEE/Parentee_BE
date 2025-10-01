using AutoMapper;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.Users;

namespace Parentee_BE.BLL.Services.Implements;

public class UserFamilyRoleService(IUnitOfWork<AppDbContext> _unitOfWork, ILogger<UserFamilyRoleService> _logger, IMapper _mapper) : BaseService<UserFamilyRoleService>(_unitOfWork, _logger), IUserFamilyRoleService
{
    public async Task<UserRoleResponse> GetFamilyIdFromByUserID(Guid userId)
    {
        var repo = _unitOfWork.GetRepository<UserFamilyRoleEntity>();
        var res = await repo.FirstOrDefaultAsync(predicate: u => u.UserId == userId);
        return _mapper.Map<UserRoleResponse>(res);
    }
}