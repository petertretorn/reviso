using Reviso.Domain.Entities;

namespace Reviso.Domain.Interfaces
{
    public interface ICalculateStrategy
    {
        decimal CalculateTotalNetDue(Project project, int remaining);
    }
}