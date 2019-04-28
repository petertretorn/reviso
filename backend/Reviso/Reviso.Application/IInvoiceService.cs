using Reviso.Application.Dtos;

namespace Reviso.Application
{
    public interface IInvoiceService
    {
        InvoiceDto CreateInvoice(int projectId);
        InvoiceDto GetInvoice(int id);
    }
}