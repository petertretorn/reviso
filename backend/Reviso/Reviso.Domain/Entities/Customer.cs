using Reviso.Domain.Interfaces;

namespace Reviso.Domain.Entities
{
    public class Customer : IEntity
    {
        public string Name { get; set; }
        public int ContractId { get; set; }
    }
}
