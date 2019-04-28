using System;
using System.Collections.Generic;
using System.Text;

namespace Reviso.Application.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ProjectName { get; set; }
        public string Customer { get; set; }
        public bool IsActive { get; set; }
        public List<RegistrationDto> Registrations { get; set; }
    }
}
