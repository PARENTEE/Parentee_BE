using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Repositories.Interfaces;

namespace Parentee_BE.BLL.Services.Implements;

public class InvoiceService(IUnitOfWork<AppDbContext> unitOfWork,
ILogger<InvoiceService> logger,
IHttpContextAccessor httpContextAccessor,
IMapper mapper) : BaseService<InvoiceService>(unitOfWork, logger, httpContextAccessor), IInvoiceService
{
    public async Task<InvoiceEntity> CreateInvoice(InvoiceEntity invoice)
    {
        try
        {
            await _unitOfWork.GetRepository<InvoiceEntity>().InsertAsync(invoice);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return  invoice;
    }

    public async Task<InvoiceEntity> UpdateInvoice(InvoiceEntity invoice)
    {
        try
        {
            _unitOfWork.GetRepository<InvoiceEntity>().UpdateAsync(invoice);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return invoice;
    }

    
}