using System;
using System.Collections.Generic;
using System.Text;

namespace Reviso.Application.Dtos
{
    public class RegistrationDto
    {
        public int Id{ get; set; }
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
    }
}
