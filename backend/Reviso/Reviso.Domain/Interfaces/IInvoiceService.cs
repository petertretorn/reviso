using Reviso.Domain.Entities;

namespace Reviso.Domain.Interfaces
{
    public interface IInvoiceService
    {
        Invoice CalculateInvoice(Project project);
    }
}