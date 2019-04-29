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

        [HttpGet("projects/{id}/registrations/")]
        public IActionResult GetRegistrationsForProject(int id)
        {
            var registrations = _registrationService.GetRegistrationsForProject(id);

            return Ok(registrations);
        }

        [HttpGet("registrations/{id}", Name = "GetRegistration")]
        public IActionResult GetRegistration(int id)
        {
            var registration = _registrationService.GetRegistration(id);

            return Ok(registration);
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
