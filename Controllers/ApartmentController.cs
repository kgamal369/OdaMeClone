using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Data;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/Apartments")]
    [ApiController]
    public class ApartmentController : ControllerBase
        {
        private readonly ApartmentService _apartmentService;
        private readonly OdaDbContext _context; // Declare OdaDbContext
        private readonly ILogger<ProjectController> _logger;

        public ApartmentController(ApartmentService apartmentService, ILogger<ProjectController> logger, OdaDbContext context)
            {
            _apartmentService = apartmentService;
            _logger = logger;
            _context = context;
            }

        [HttpGet]
        public ActionResult<IEnumerable<ApartmentDTO>> GetAllApartments()
            {
            var apartments = _apartmentService.GetAllApartments();
            if (apartments == null)
                {
                return NotFound("No apartments found.");
                }
            return Ok(_apartmentService.GetAllApartments());
            }

        [HttpGet("{id}")]
        public ActionResult<ApartmentDTO> GetApartmentById(Guid id)
            {
            try
                {
                var apartment = _apartmentService.GetApartmentById(id);
                if (apartment == null)
                    {
                    return NotFound($"Apartment with ID {id} not found.");
                    }
                return Ok(apartment);
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public ActionResult AddApartment(Apartment apartmentDTO)
            {
            if (apartmentDTO == null || !ModelState.IsValid)
                {
                return BadRequest("Invalid apartment data.");
                }
            // Ensure the apartment is assigned to a valid existing project
            if (!_apartmentService.IsValidProject(apartmentDTO.ProjectId))
                {
                return BadRequest("The specified project does not exist.");
                }

            _apartmentService.AddApartment(apartmentDTO);
            return CreatedAtAction(nameof(GetApartmentById), new { id = apartmentDTO.ApartmentId }, apartmentDTO);
            }

        [HttpPut("{id}")]
        public ActionResult UpdateApartment(Guid id, Apartment apartmentDTO)
            {
            if (apartmentDTO == null || !ModelState.IsValid)
                {
                return BadRequest("Invalid apartment data.");
                }

            try
                {
                _apartmentService.UpdateApartment(id, apartmentDTO);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpDelete("{id}")]
        public ActionResult DeleteApartment(Guid id)
            {
            try
                {
                _apartmentService.DeleteApartment(id);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }
        }
    }
