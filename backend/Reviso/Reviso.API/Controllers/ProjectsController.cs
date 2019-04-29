using Microsoft.AspNetCore.Mvc;
using Reviso.Application;
using Reviso.Application.Dtos;

namespace Reviso.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            this._projectService = projectService;
        }

        [HttpGet()]
        public IActionResult GetProjects()
        {
            var projects = _projectService.GetProjects();

            return Ok(projects);
        }

        [HttpPost()]
        public IActionResult CreateProject([FromBody] CreateEditProjectDto dto)
        {
            var projectDto = _projectService.CreateProject(dto);

            return Ok(projectDto);
        }

        [HttpGet("{id}/invoice", Name = "GetInvoice")]
        public IActionResult GetInvoice(int id)
        {
            var invoice = _projectService.GetInvoice(id);

            return Ok(invoice);
        }

        [HttpPost("{id}/invoice")]
        public IActionResult InvoiceProject(int id)
        {
            var invoice = _projectService.InvoiceProject(id);

            return CreatedAtRoute("GetInvoice", new { id = invoice.Id }, invoice);
        }
    }
}