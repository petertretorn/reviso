using Reviso.Domain.Dtos;
using Reviso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviso.Domain.Factories
{
    public class ProjectFactory
    {
        public static Project Create(CreateEditProjectDto dto)
        {
            var project = new Project
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

            return project;
        }
    }
}
