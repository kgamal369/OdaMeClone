using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
        {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
            {
            _customerService = customerService;
            }

        [HttpGet]
        public IActionResult GetAllCustomers()
            {
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
            }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(Guid id)
            {
            try
                {
                var customer = _customerService.GetCustomerById(id);
                return Ok(customer);
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customerDTO)
            {
            if (customerDTO == null)
                {
                return BadRequest("Invalid customer data.");
                }

            _customerService.AddCustomer(customerDTO);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customerDTO.CustomerId }, customerDTO);
            }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(Guid id, [FromBody] Customer customerDTO)
            {
            try
                {
                _customerService.UpdateCustomer(id, customerDTO);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(Guid id)
            {
            try
                {
                _customerService.DeleteCustomer(id);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }
        }
    }
