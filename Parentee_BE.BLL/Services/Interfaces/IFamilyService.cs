using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Family;
using Parentee_BE.DAL.Data.ResponseDTO.Family;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IFamilyService
{
    Task<GetFamilyResponse> GetFamilyById(Guid id);
    Task<GetFamilyDetailResponse> GetFamilyDetailById(Guid id);
    Task<GetFamilyResponse> CreateFamily(string name);
    Task<FamilyEntity> AddMemberForFamily(Guid familyId, UserFamilyRoleEntity userFamilyRoleEntity);
    Task<bool> UpdateInvitation(Guid id, bool isAccepted);
    Task<GetFamilyResponse> UpdateFamily(Guid id, UpdateFamilyRequest requestDto);
    Task<bool> DisableFamily(Guid id);
    Task<bool> DeleteFamily(Guid id);
}