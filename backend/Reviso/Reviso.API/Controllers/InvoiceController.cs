using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reviso.Application;

namespace Reviso.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IProjectService _invoiceService;

        public InvoiceController(IProjectService invoiceService)
        {
            this._invoiceService = invoiceService;
        }

        [HttpGet("projects/{id}/invoice", Name = "GetInvoice")]
        public IActionResult GetInvoice(int id)
        {
            var invoice = _invoiceService.GetInvoice(id);

            return Ok(invoice);
        }

        [HttpPost("projects/{id}/invoice")]
        public IActionResult InvoiceProject(int id)
        {
            var invoice = _invoiceService.InvoiceProject(id);

            return CreatedAtRoute("GetInvoice", new { id = invoice.Id }, invoice);
        }
    }
}