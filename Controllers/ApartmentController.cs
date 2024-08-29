using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
        {
        private readonly ApartmentService _apartmentService;

        public ApartmentController(ApartmentService apartmentService)
            {
            _apartmentService = apartmentService;
            }

        [HttpGet]
        public ActionResult<IEnumerable<ApartmentDTO>> GetAllApartments()
            {
            return Ok(_apartmentService.GetAllApartments());
            }

        [HttpGet("{id}")]
        public ActionResult<ApartmentDTO> GetApartmentById(Guid id)
            {
            try
                {
                return Ok(_apartmentService.GetApartmentById(id));
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public ActionResult AddApartment(ApartmentDTO apartmentDTO)
            {
            _apartmentService.AddApartment(apartmentDTO);
            return CreatedAtAction(nameof(GetApartmentById), new { id = apartmentDTO.ApartmentId }, apartmentDTO);
            }

        [HttpPut("{id}")]
        public ActionResult UpdateApartment(Guid id, ApartmentDTO apartmentDTO)
            {
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
