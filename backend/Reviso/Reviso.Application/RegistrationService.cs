using System.Collections.Generic;
using System.Linq;
using Reviso.Domain.Dtos;
using Reviso.Domain.Entities;
using Reviso.Domain.Interfaces;


namespace Reviso.Application
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<Project> _projectRepo;
        private readonly IRepository<TimeRegistration> _registrationRepo;

        public RegistrationService(IRepository<Project> projectRepo, IRepository<TimeRegistration> registrationRepo)
        {
            this._projectRepo = projectRepo;
            this._registrationRepo = registrationRepo;
        }

        public int AddRegistration(RegistrationDto registration)
        {
            var project = _projectRepo.GetByIdIncluding(registration.ProjectId, p => p.TimeRegistrations);

            project.AddTimeRegistration(new TimeRegistration()
            {
                Date = registration.Date,
                Hours = registration.Hours
            });

            _projectRepo.Update(project);

            return project.TimeRegistrations.Last().Id;
        }

        public void DeleteRegistration(int id)
        {
            _registrationRepo.Delete(id);
        }

        public RegistrationDto GetRegistration(int id)
        {
            var registration = _registrationRepo.GetById(id);

            return Mappers.MapToRegistrationDto(registration);
        }

        public IEnumerable<RegistrationDto> GetRegistrationsForProject(int projectId)
        {
            var project = _projectRepo.GetByIdIncluding(projectId, p => p.TimeRegistrations);

            return project.TimeRegistrations
                .Select(Mappers.MapToRegistrationDto)
                .ToList();
        }
    }
}
