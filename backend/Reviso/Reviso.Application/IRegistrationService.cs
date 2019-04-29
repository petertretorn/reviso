using Reviso.Domain.Dtos;
using System.Collections.Generic;

namespace Reviso.Application
{
    public interface IRegistrationService
    {
        int AddRegistration(RegistrationDto registration);
        IEnumerable<RegistrationDto> GetRegistrationsForProject(int projectId);
        RegistrationDto GetRegistration(int id);
        IEnumerable<ProjectDto> GetProjects();
        void DeleteRegistration(int registration);
    }
}