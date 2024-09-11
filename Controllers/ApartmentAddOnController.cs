using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/ApartmentAddOns")]
    [ApiController]
    public class ApartmentAddOnController : ControllerBase
        {
        private readonly ApartmentAddOnService _apartmentAddOnService;

        public ApartmentAddOnController(ApartmentAddOnService apartmentAddOnService)
            {
            _apartmentAddOnService = apartmentAddOnService;
            }

        [HttpGet]
        public ActionResult<IEnumerable<ApartmentAddOnDTO>> GetAllApartmentAddOns()
            {
            return Ok(_apartmentAddOnService.GetAllApartmentAddOns());
            }

        [HttpGet("{apartmentId}/{addOnId}")]
        public ActionResult<ApartmentAddOnDTO> GetApartmentAddOnById(Guid apartmentId, Guid addOnId)
            {
            try
                {
                return Ok(_apartmentAddOnService.GetApartmentAddOnById(apartmentId, addOnId));
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public ActionResult AddApartmentAddOn(ApartmentAddOn apartmentAddOnDTO)
            {
            _apartmentAddOnService.AddApartmentAddOn(apartmentAddOnDTO);
            return CreatedAtAction(nameof(GetApartmentAddOnById), new { apartmentId = apartmentAddOnDTO.ApartmentId, addOnId = apartmentAddOnDTO.AddOnId }, apartmentAddOnDTO);
            }

        [HttpPut("{apartmentId}/{addOnId}")]
        public ActionResult UpdateApartmentAddOn(Guid apartmentId, Guid addOnId, ApartmentAddOn apartmentAddOnDTO)
            {
            try
                {
                _apartmentAddOnService.UpdateApartmentAddOn(apartmentId, addOnId, apartmentAddOnDTO);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpDelete("{apartmentId}/{addOnId}")]
        public ActionResult DeleteApartmentAddOn(Guid apartmentId, Guid addOnId)
            {
            try
                {
                _apartmentAddOnService.DeleteApartmentAddOn(apartmentId, addOnId);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }
        }
    }
