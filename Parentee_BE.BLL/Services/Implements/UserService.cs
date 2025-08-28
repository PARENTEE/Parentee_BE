using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.Users;
using Parentee_BE.DAL.Data.ResponseDTO.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Helpers;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;

namespace Parentee_BE.BLL.Services.Implements;

public class UserService(
    IUnitOfWork<AppDbContext> unitOfWork, 
    ILogger<UserEntity> logger,
    IHttpContextAccessor? httpContextAccessor,
    IMapper mapper)
    : BaseService<UserEntity>(unitOfWork, logger, httpContextAccessor), IUserService
{
    public Task<UserEntity> GetUser()
    {
        throw new NotImplementedException();
    }

    public async Task<GetUserResponseDTO> GetCurrentUser()
    {
        return await GetUserById(GetCurrentAccountIdThroughToken());
    }

    public async Task<ICollection<GetUserResponseDTO>> GetManyUsers(int pageNumber, int pageSize)
    {
        var Users = await unitOfWork.GetRepository<UserEntity>().GetPagingListAsync(
            pageIndex: pageNumber,
            pageSize: pageSize);

        return Users.Items.Select(mapper.Map<UserEntity, GetUserResponseDTO>).ToList();
    }

    public async Task<GetUserResponseDTO> GetUserById(Guid id)
    {
        try
        {
            var User = await unitOfWork.GetRepository<UserEntity>().FirstOrDefaultAsync(
                predicate: a => a.Id == id,
                include: a => a.Include(a => a.UserFamilyRole));
            return mapper.Map<UserEntity, GetUserResponseDTO>(User);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting claim: {Message}", ex.Message);
            throw;
        }
    }

    public async Task<GetUserResponseDTO> CreateUser(CreateUserRequestDTO requestDto)
    {
        var User = mapper.Map<CreateUserRequestDTO, UserEntity>(requestDto);
        await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            // Check if Role exists
            var findRoleResult = await unitOfWork.GetRepository<UserFamilyRoleEntity>().FirstOrDefaultAsync(
                predicate: r => r.Role.ToString() == requestDto.Role.ToLower());
            if (findRoleResult == null) throw new NotFoundException("Role not found!");
            User.UserFamilyRole = findRoleResult;
            
            // Hash Password
            var hashedPassword = PasswordHelper.HashPassword(requestDto.Password);
            User.Password = hashedPassword;
            
            // Add User
            await unitOfWork.GetRepository<UserEntity>().InsertAsync(User);
            
        });
        return mapper.Map<UserEntity, GetUserResponseDTO>(User);;
    }

    public async Task<GetUserResponseDTO> UpdateUser(Guid id, UpdateUserRequestDTO requestDto)
    {
        // Check if User exists
        var User = await unitOfWork.GetRepository<UserEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id,
            include: a => a.Include(a => a.UserFamilyRole));
        if (User == null) throw new NotFoundException("User not found!");
        
        // Update User
        await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            // Map UpdateUserRequestDTO to UserEntity
            mapper.Map(requestDto, User);
            
            // Update
            unitOfWork.GetRepository<UserEntity>().UpdateAsync(User);
            
        });
        return mapper.Map<UserEntity, GetUserResponseDTO>(User);
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var User = await unitOfWork.GetRepository<UserEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id,
            include: a => a.Include(a => a.UserFamilyRole));
        if (User == null) throw new NotFoundException("User not found!");
        
        // Delete User
        await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
           unitOfWork.GetRepository<UserEntity>().Delete(User);
            
        });
        return true;
    }
}