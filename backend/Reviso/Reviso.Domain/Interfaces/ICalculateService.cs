using Reviso.Domain.Entities;

namespace Reviso.Domain.Interfaces
{
    public interface ICalculateService
    {
        Invoice CalculateInvoice(Project project);
    }
}