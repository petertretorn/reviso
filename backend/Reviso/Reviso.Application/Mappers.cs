using Reviso.Application.Dtos;
using Reviso.Domain.Entities;
using System;
using System.Collections.Generic;
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
                Invoice = (p.Invoice is null) ? null: MapToInvoiceDto(p.Invoice),
                Registrations = p.TimeRegistrations.Select(Mappers.MapToRegistrationDto).ToList()
            };
        }

        public static Project MapToProject(CreateEditProjectDto dto)
        {
            return new Project
            {
                Name = dto.Project,
                Start = dto.Start,
                Contract = new Contract
                {
                    BaseRate = dto.BaseRate,
                    VatRate = dto.VatRate,
                    RateIntervals = new List<RateInterval>
                    {
                        new RateInterval
                        {
                            FromHours = 0,
                            ToHours = Int32.MaxValue,
                            DiscountFactor = 0
                        }
                    },
                    Customer = new Customer
                    {
                        Name = dto.Customer
                    }
                }
            };
        }

        public static InvoiceDto MapToInvoiceDto(Invoice invoice)
        {
            return new InvoiceDto
            {
                Customer = invoice.Customer,
                InvoiceDate = invoice.InvoiceDate,
                Net = invoice.Net,
                Project = invoice.Project,
                Vat = invoice.Vat
            };
        }
    }
}
