using Reviso.Application.Dtos;
using System.Collections.Generic;

namespace Reviso.Application
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetProjects();
        ProjectDto CreateProject(CreateEditProjectDto dto);
        InvoiceDto InvoiceProject(int projectId);
        InvoiceDto GetInvoice(int id);
    }
}