using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
        {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
            {
            _invoiceService = invoiceService;
            }

        [HttpGet]
        public IActionResult GetAllInvoices()
            {
            var invoices = _invoiceService.GetAllInvoices();
            return Ok(invoices);
            }

        [HttpGet("{id}")]
        public IActionResult GetInvoiceById(Guid id)
            {
            try
                {
                var invoice = _invoiceService.GetInvoiceById(id);
                return Ok(invoice);
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public IActionResult AddInvoice([FromBody] InvoiceDTO invoiceDTO)
            {
            if (invoiceDTO == null)
                {
                return BadRequest("Invalid invoice data.");
                }

            _invoiceService.AddInvoice(invoiceDTO);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoiceDTO.InvoiceId }, invoiceDTO);
            }

        [HttpPut("{id}")]
        public IActionResult UpdateInvoice(Guid id, [FromBody] InvoiceDTO invoiceDTO)
            {
            try
                {
                _invoiceService.UpdateInvoice(id, invoiceDTO);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(Guid id)
            {
            try
                {
                _invoiceService.DeleteInvoice(id);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost("{id}/applypayment")]
        public IActionResult ApplyPayment(Guid id, [FromBody] decimal amountPaid)
            {
            try
                {
                _invoiceService.ApplyPayment(id, amountPaid);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            catch (ArgumentException ex)
                {
                return BadRequest(ex.Message);
                }
            }
        }
    }
