using Reviso.Application.Dtos;
using Reviso.Domain.Entities;
using Reviso.Domain.Interfaces;
using System;

namespace Reviso.Application
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ICalculateService this_calculateService;
        private readonly IRepository<Project> _projectRepo;

        public InvoiceService(ICalculateService calculateService, IRepository<Project> projectRepo)
        {
            this_calculateService = calculateService;
            this._projectRepo = projectRepo;
        }
        public InvoiceDto CreateInvoice(int projectId)
        {
            var project = _projectRepo.GetByIdIncluding(projectId,
                p => p.TimeRegistrations, 
                p => p.Contract.Customer, 
                p => p.Contract.RateIntervals
            );

            project.Close();
            //_projectRepo.Update(project);

            var invoice = this_calculateService.CalculateInvoice(project);

            return new InvoiceDto
            {
                Id = invoice.Id,
                Customer = invoice.Customer.Name,
                InvoiceDate = invoice.InvoiceDate,
                Net = invoice.Net,
                Project = invoice.Project.Name,
                Vat = invoice.Vat
            };
        }

        public InvoiceDto GetInvoice(int id)
        {
            throw new NotImplementedException();
        }
    }
}
