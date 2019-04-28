using Reviso.Domain.Interfaces;
using System;

namespace Reviso.Domain.Entities
{
    public class Invoice : IEntity
    {
        public static Invoice Create(Project project, decimal netAmmount) => new Invoice()
        {
            Net = netAmmount,
            Customer = project.Contract.Customer,
            Vat = project.Contract.VatRate * netAmmount,
            InvoiceDate = DateTime.Now.Date,
        };

        public Customer Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        public string Description { get; set; }
        
        public decimal Net { get; set; }
        
        public decimal Vat { get; set; }

        public decimal Gross => Net + Vat;
    }
}
