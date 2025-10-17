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

        public async Task<GetFamilyResponse> CreateFamily(CreateFamilyRequest requestDto)
        {
            var familyEntity = mapper.Map<CreateFamilyRequest, FamilyEntity>(requestDto);
            await unitOfWork.ExecuteInTransactionAsync( async () =>
            {
                await unitOfWork.GetRepository<FamilyEntity>().InsertAsync(familyEntity);
                
                // // Father user role
                // var userFamilyRoleEntities = new List<UserFamilyRoleEntity>
                // {
                //     new()
                //     {
                //         UserId = requestDto.FatherUserId,
                //         FamilyId = familyEntity.Id,
                //         Role = FamilyRole.Father
                //     },
                //     new()
                //     {
                //         UserId = requestDto.MotherUserId,
                //         FamilyId = familyEntity.Id,
                //         Role = FamilyRole.Mother
                //     }
                // };
                //
                // await unitOfWork.GetRepository<UserFamilyRoleEntity>()
                //     .InsertRangeAsync(userFamilyRoleEntities);
            });
            
            return mapper.Map<GetFamilyResponse>(familyEntity);
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
