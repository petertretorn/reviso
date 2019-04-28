using Reviso.Domain.Entities;
using Reviso.Domain.Interfaces;
using System;
using System.Linq;

namespace Reviso.Domain.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly ICalculateStrategy _strategy;

        public CalculateService(ICalculateStrategy strategy)
        {
            this._strategy = strategy;
        }

        public Invoice CalculateInvoice(Project project)
        {
            if (project.IsActive) throw new ArgumentException("Cannot invoice an active project");

            int totalHours = GetTotalHours(project);

            decimal netAmmount = this._strategy.CalculateTotalNetDue(project, totalHours);

            return Invoice.Create(project, netAmmount);
        }

        private int GetTotalHours(Project project) => 
            project.TimeRegistrations
                .Select(r => r.Hours)
                .Sum();
        
    }
}
