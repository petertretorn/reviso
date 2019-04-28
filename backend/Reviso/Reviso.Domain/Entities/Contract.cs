using Reviso.Domain.Interfaces;
using System.Collections.Generic;

namespace Reviso.Domain.Entities
{
    public class Contract : IEntity
    {
        public Customer Customer { get; set; }
        public int ProjectId { get; set; }
        public decimal BaseRate { get; set; }
        public decimal VatRate { get; set; }
        public IEnumerable<RateInterval> RateIntervals { get; set; }
    }

    public class RateInterval : IEntity
    {
        public int ContractId { get; set; }
        public int FromHours { get; set; }
        public int ToHours { get; set; }
        public decimal DiscountFactor { get; set; }
    }
}
