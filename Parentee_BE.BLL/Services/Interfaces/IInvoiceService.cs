using Parentee_BE.DAL.Data.Entities;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceEntity> CreateInvoice(InvoiceEntity invoice);
    Task<InvoiceEntity> UpdateInvoice(InvoiceEntity invoice);
    
}