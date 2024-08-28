using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = _paymentService.GetAllPayments();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentById(Guid id)
        {
            try
            {
                var payment = _paymentService.GetPaymentById(id);
                return Ok(payment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddPayment([FromBody] PaymentDTO paymentDTO)
        {
            if (paymentDTO == null)
            {
                return BadRequest("Invalid payment data.");
            }

            _paymentService.AddPayment(paymentDTO);
            return CreatedAtAction(nameof(GetPaymentById), new { id = paymentDTO.PaymentId }, paymentDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(Guid id, [FromBody] PaymentDTO paymentDTO)
        {
            try
            {
                _paymentService.UpdatePayment(id, paymentDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePayment(Guid id)
        {
            try
            {
                _paymentService.DeletePayment(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
