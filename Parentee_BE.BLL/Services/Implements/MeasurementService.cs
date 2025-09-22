using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.RequestDTO.Measurement;
using Parentee_BE.DAL.Data.ResponseDTO.Measurement;
using Parentee_BE.DAL.Data.Repositories.Interfaces;

namespace Parentee_BE.BLL.Services.Implements
{
    public class MeasurementService(
        IUnitOfWork<AppDbContext> unitOfWork,
        ILogger<MeasurementService> logger,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    ) : BaseService<MeasurementService>(unitOfWork, logger, httpContextAccessor), IMeasurementService
    {
        public async Task<GetMeasurementResponse> GetMeasurementById(Guid id)
        {
            var entity = await unitOfWork.GetRepository<MeasurementEntity>().FirstOrDefaultAsync(
                predicate: a => a.Id == id
            );
            if (entity == null) throw new NotFoundException("Measurement not found!");
            return mapper.Map<GetMeasurementResponse>(entity);
        }

        public async Task<GetMeasurementResponse> CreateMeasurement(CreateMeasurementRequest requestDto)
        {
            var entity = mapper.Map<CreateMeasurementRequest, MeasurementEntity>(requestDto);
            await unitOfWork.GetRepository<MeasurementEntity>().InsertAsync(entity);
            return mapper.Map<MeasurementEntity, GetMeasurementResponse>(entity);
        }

        public async Task<GetMeasurementResponse> UpdateMeasurement(Guid id, UpdateMeasurementRequest requestDto)
        {
            var entity = await unitOfWork.GetRepository<MeasurementEntity>().FirstOrDefaultAsync(
                predicate: a => a.Id == id
            );
            if (entity == null) throw new NotFoundException("Measurement not found!");

            mapper.Map(requestDto, entity);
            unitOfWork.GetRepository<MeasurementEntity>().UpdateAsync(entity);

            return mapper.Map<MeasurementEntity, GetMeasurementResponse>(entity);
        }

        public async Task<bool> DeleteMeasurement(Guid id)
        {
            var entity = await unitOfWork.GetRepository<MeasurementEntity>().FirstOrDefaultAsync(
                predicate: a => a.Id == id
            );
            if (entity == null) throw new NotFoundException("Measurement not found!");

            unitOfWork.GetRepository<MeasurementEntity>().Delete(entity);
            return true;
        }
    }
}
