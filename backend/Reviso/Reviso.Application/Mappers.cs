using Reviso.Domain.Dtos;
using Reviso.Domain.Entities;
using System.Linq;

namespace Reviso.Application
{
    public class Mappers
    {
        public static RegistrationDto MapToRegistrationDto(TimeRegistration registration)
        {
            return new RegistrationDto()
            {
                Id = registration.Id,
                Date = registration.Date,
                Hours = registration.Hours,
                ProjectId = registration.ProjectId
            };
        }

        public static ProjectDto MapToProjectDto(Project p)
        {
            return new ProjectDto
            {
                Id = p.Id,
                Start = p.Start,
                End = p.End,
                IsActive = p.IsActive,
                ProjectName = p.Name,
                Customer = p.Contract.Customer.Name,
                Registrations = p.TimeRegistrations.Select(Mappers.MapToRegistrationDto).ToList()
            };
        }
    }
}
