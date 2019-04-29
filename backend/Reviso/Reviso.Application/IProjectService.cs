using Reviso.Domain.Dtos;

namespace Reviso.Application
{
    public interface IProjectService
    {
        InvoiceDto InvoiceProject(int projectId);
        InvoiceDto GetInvoice(int id);
    }
}