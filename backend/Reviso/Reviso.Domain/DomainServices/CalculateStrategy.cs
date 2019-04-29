using System.Linq;

using Reviso.Domain.Entities;
using Reviso.Domain.Interfaces;

namespace Reviso.Domain.Services
{
    public class CalculateStrategy : ICalculateStrategy
    {
        public decimal CalculateTotalNetDue(Project project, int remaining)
        {
            return project.Contract.RateIntervals
                .Aggregate(0m, (aggregate, current) =>
                {
                    var interval = current.ToHours - current.FromHours;

                    var hours = (interval <= remaining)
                        ? current.ToHours - current.FromHours
                        : remaining;

                    remaining = remaining - hours;

                    return aggregate + CalculateInterval(hours, current.DiscountFactor, project.Contract.BaseRate);
                });
        }

        private decimal CalculateInterval(int hours, decimal discountFactor, decimal baseRate)
            => hours * (1 - discountFactor) * baseRate;
    }
}
