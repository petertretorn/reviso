using Reviso.Domain.Entities;
using Reviso.Domain.Interfaces;

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
        public void CreateInvoice(int projectId)
        {
            var project = _projectRepo.GetById(projectId);

            project.Close();
            _projectRepo.Update(project);

            var invoice = this_calculateService.CalculateInvoice(project);
        }
    }
}
