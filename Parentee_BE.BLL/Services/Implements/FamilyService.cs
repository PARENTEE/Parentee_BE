using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.RequestDTO.Family;
using Parentee_BE.DAL.Data.ResponseDTO.Family;
using Parentee_BE.DAL.Data.Repositories.Interfaces;

namespace Parentee_BE.BLL.Services.Implements
{
    public class FamilyService(
        IUnitOfWork<AppDbContext> unitOfWork,
        ILogger<FamilyService> logger,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    ) : BaseService<FamilyService>(unitOfWork, logger, httpContextAccessor), IFamilyService
    {
        public async Task<GetFamilyResponse> GetFamilyById(Guid id)
        {
            var entity = await unitOfWork.GetRepository<FamilyEntity>()
                .FirstOrDefaultAsync(predicate: f => f.Id == id,
                    include: q => q.Include(f => f.UserFamilyRoles));

            if (entity == null) throw new NotFoundException("Family not found!");

            return mapper.Map<GetFamilyResponse>(entity);
        }

        public async Task<GetFamilyDetailResponse> GetFamilyDetailById(Guid id)
        {
            var entity = await unitOfWork.GetRepository<FamilyEntity>()
                .FirstOrDefaultAsync(predicate: f => f.Id == id,
                    include: q => q.Include(f => f.UserFamilyRoles)
                        .ThenInclude(ufre => ufre.User));

            if (entity == null) throw new NotFoundException("Family not found!");

            return mapper.Map<GetFamilyDetailResponse>(entity);
        }

        public async Task<GetFamilyResponse> CreateFamily(string name)
        {
            var userId = GetCurrentAccountIdThroughToken();

            var activeFamilyCount = await unitOfWork.GetRepository<UserFamilyRoleEntity>()
                .CountAsync(predicate: ufre => ufre.UserId == userId);
            if (activeFamilyCount > 0)
                throw new BusinessException("Một người chỉ có thể tham gia vào một gia đình!");

            var user = await unitOfWork.GetRepository<UserEntity>()
                .FirstOrDefaultAsync(predicate: u => u.Id == userId);

            var familyRole = FamilyRole.Father;
            if (user.Gender == Gender.Female) familyRole = FamilyRole.Mother;
            
            var familyEntity = new FamilyEntity()
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedBy = userId,
                UserFamilyRoles = [
                    new UserFamilyRoleEntity { UserId = userId, Role = familyRole, CreatedAt = DateTime.UtcNow, InvitationStatus = InvitationStatus.Accepted },
                ]
            };
            // Create family
            await unitOfWork.GetRepository<FamilyEntity>().InsertAsync(familyEntity);

            return mapper.Map<GetFamilyResponse>(familyEntity);
        }
        
        public async Task<FamilyEntity> AddMemberForFamily(Guid familyId, UserFamilyRoleEntity userFamilyRoleEntity)
        {
            // Get family
            var familyEntity = await unitOfWork.GetRepository<FamilyEntity>()
                .FirstOrDefaultAsync(predicate: u => u.Id == familyId,
                    include: q => q.Include(f => f.UserFamilyRoles));
            if(familyEntity == null) throw new NotFoundException("Gia đình không tìm thấy!");
            
            // Check if user have authorization to add member
            if (familyEntity.CreatedBy != GetCurrentAccountIdThroughToken())
                throw new BusinessException("Chỉ những người tạo mới có thể thêm thành viên!");
            
            // Check if user exist
            var addedUserEntity = await unitOfWork.GetRepository<UserEntity>()
                .FirstOrDefaultAsync(predicate: u => u.Id == userFamilyRoleEntity.UserId);
            if(addedUserEntity == null) throw new NotFoundException("Người muốn thêm vào không tìm thấy!");
            
            // Check if user already added
            if (familyEntity.UserFamilyRoles.Any(entity => entity.UserId == addedUserEntity.Id))
                throw new BusinessException("Người này đã được thêm vào!");

            // Assign Member
            userFamilyRoleEntity.InvitationStatus = InvitationStatus.InProcessing;
            familyEntity.UserFamilyRoles.Add(userFamilyRoleEntity);
            unitOfWork.GetRepository<FamilyEntity>().UpdateAsync(familyEntity);

            return familyEntity;
        }

        public async Task<GetFamilyResponse> UpdateFamily(Guid id, UpdateFamilyRequest requestDto)
        {
            var entity = await unitOfWork.GetRepository<FamilyEntity>()
                .FirstOrDefaultAsync(predicate: f => f.Id == id);

            if (entity == null) throw new NotFoundException("Family not found!");

            mapper.Map(requestDto, entity);
            unitOfWork.GetRepository<FamilyEntity>().UpdateAsync(entity);

            return mapper.Map<GetFamilyResponse>(entity);
        }

        public async Task<bool> DisableFamily(Guid id)
        {
            var entity = await unitOfWork.GetRepository<FamilyEntity>()
                .FirstOrDefaultAsync(predicate: f => f.Id == id);

            if (entity == null) throw new NotFoundException("Family not found!");
            entity.IsDisable = true;

            unitOfWork.GetRepository<FamilyEntity>().UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteFamily(Guid id)
        {
            var entity = await unitOfWork.GetRepository<FamilyEntity>()
                .FirstOrDefaultAsync(predicate: f => f.Id == id);

            if (entity == null) throw new NotFoundException("Family not found!");

            unitOfWork.GetRepository<FamilyEntity>().Delete(entity);
            return true;
        }
    }
}