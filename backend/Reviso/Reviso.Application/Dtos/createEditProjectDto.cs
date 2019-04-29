using System;

namespace Reviso.Application.Dtos
{
    public class CreateEditProjectDto
    {
        public int Id { get; set; }
        public string Project { get; set; }
        public string Customer { get; set; }
        public DateTime Start { get; set; }
        public decimal BaseRate { get; set; }
        public decimal VatRate { get; set; }
    }
}
