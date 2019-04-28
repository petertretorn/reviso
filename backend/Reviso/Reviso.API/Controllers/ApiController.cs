using Microsoft.AspNetCore.Mvc;
using Reviso.Application;
using Reviso.Application.Dtos;

namespace Reviso.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public ApiController(IRegistrationService registrationService)
        {
            this._registrationService = registrationService;
        }

        [HttpGet("projects")]
        public IActionResult GetProjects()
        {
            var payload = _registrationService.GetProjects();

            return Ok(payload);
        }

        [HttpGet("projects/{id}/registrations/")]
        public IActionResult GetRegistrationsForProject(int id)
        {
            var payload = _registrationService.GetRegistrationsForProject(id);

            return Ok(payload);
        }

        [HttpGet("registrations/{id}", Name = "GetRegistration")]
        public IActionResult GetRegistration(int id)
        {
            var payload = _registrationService.GetRegistration(id);

            return Ok(payload);
        }

        [HttpPost("projects/{id}/registrations/")]
        public IActionResult AddRegistrationToProject([FromBody] RegistrationDto registration)
        {
            var registrationId = _registrationService.AddRegistration(registration);

            return CreatedAtRoute("GetRegistration", new { id = registrationId }, registration);
        }

        [HttpDelete("projects/{id}/registrations/{registrationId}")]
        public IActionResult DeleteRegistration(int registrationId)
        {
            _registrationService.DeleteRegistration(registrationId);

            return NoContent();
        }
    }
}
