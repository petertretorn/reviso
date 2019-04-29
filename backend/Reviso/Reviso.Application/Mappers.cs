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
                Invoice = p.Invoice,
                Registrations = p.TimeRegistrations.Select(Mappers.MapToRegistrationDto).ToList()
            };
        }

        public static InvoiceDto MapToInvoiceDto(Invoice invoice)
        {
            return new InvoiceDto
            {
                Id = invoice.Id,
                Customer = invoice.Customer.Name,
                InvoiceDate = invoice.InvoiceDate,
                Net = invoice.Net,
                Project = invoice.Project,
                Vat = invoice.Vat
            };
        }
    }
}
