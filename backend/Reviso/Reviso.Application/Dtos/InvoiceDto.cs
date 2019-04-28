using System;
using System.Collections.Generic;
using System.Text;

namespace Reviso.Application.Dtos
{
    public class InvoiceDto
    {
        public string Customer { get; set; }
        public string Project { get; set; }
        public decimal Net { get; set; }
        public decimal Vat { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public object Id { get; set; }
    }
}
