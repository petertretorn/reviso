using Reviso.Application.Dtos;
using Reviso.Domain.Entities;
using Reviso.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reviso.Application
{
    public class ProjectService : IProjectService
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IRepository<Project> _projectRepo;

        public ProjectService(IInvoiceService invoiceService, IRepository<Project> projectRepo)
        {
            this._invoiceService = invoiceService;
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
            var project = Mappers.MapToProject(dto);

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

            var invoice = _invoiceService.CalculateInvoice(project);
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
