using Reviso.Domain.Dtos;
using Reviso.Domain.Entities;
using Reviso.Domain.Factories;
using Reviso.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reviso.Application
{
    public class ProjectService : IProjectService
    {
        private readonly ICalculateService this_calculateService;
        private readonly IRepository<Project> _projectRepo;

        public ProjectService(ICalculateService calculateService, IRepository<Project> projectRepo)
        {
            this_calculateService = calculateService;
            this._projectRepo = projectRepo;
        }

        public IEnumerable<ProjectDto> GetProjects()
        {
            var projects = _projectRepo.GetAllIncluding(
                p => p.TimeRegistrations, 
                p => p.Contract.Customer, 
                p => p.Invoice);

            return projects.Select(Mappers.MapToProjectDto);
        }
        
        public ProjectDto CreateProject(CreateEditProjectDto dto)
        {
            var project = ProjectFactory.Create(dto);

            _projectRepo.Add(project);

            dto.Id = project.Id;

            return Mappers.MapToProjectDto(project);
        }

        public InvoiceDto InvoiceProject(int projectId)
        {
            var project = _projectRepo.GetByIdIncluding(projectId,
                p => p.TimeRegistrations,
                p => p.Contract.Customer,
                p => p.Contract.RateIntervals
            );

            project.Close();

            var invoice = this_calculateService.CalculateInvoice(project);
            project.Invoice = invoice;

            _projectRepo.Update(project);

            return Mappers.MapToInvoiceDto(invoice);
        }

        public InvoiceDto GetInvoice(int id)
        {
            throw new NotImplementedException();
        }
    }
}
