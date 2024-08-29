using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class AddOnController : ControllerBase
        {
        private readonly AddOnService _addOnService;

        public AddOnController(AddOnService addOnService)
            {
            _addOnService = addOnService;
            }

        [HttpGet]
        public IActionResult GetAllAddOns()
            {
            var addOns = _addOnService.GetAllAddOns();
            return Ok(addOns);
            }

        [HttpGet("{id}")]
        public IActionResult GetAddOnById(Guid id)
            {
            try
                {
                var addOn = _addOnService.GetAddOnById(id);
                return Ok(addOn);
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public IActionResult AddAddOn([FromBody] AddOnDTO addOnDTO)
            {
            if (addOnDTO == null)
                {
                return BadRequest("Invalid addon data.");
                }

            _addOnService.AddAddOn(addOnDTO);
            return CreatedAtAction(nameof(GetAddOnById), new { id = addOnDTO.AddOnId }, addOnDTO);
            }

        [HttpPut("{id}")]
        public IActionResult UpdateAddOn(Guid id, [FromBody] AddOnDTO addOnDTO)
            {
            try
                {
                _addOnService.UpdateAddOn(id, addOnDTO);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddOn(Guid id)
            {
            try
                {
                _addOnService.DeleteAddOn(id);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }
        }
    }
