using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
        {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
            {
            _bookingService = bookingService;
            }

        [HttpGet]
        public IActionResult GetAllBookings()
            {
            var bookings = _bookingService.GetAllBookings();
            return Ok(bookings);
            }

        [HttpGet("{id}")]
        public IActionResult GetBookingById(Guid id)
            {
            try
                {
                var booking = _bookingService.GetBookingById(id);
                return Ok(booking);
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public IActionResult AddBooking([FromBody] BookingDTO bookingDTO)
            {
            if (bookingDTO == null || !ModelState.IsValid)
                {
                return BadRequest("Invalid booking data.");
                }

            // Ensure that the selected apartment, package, and add-ons are valid
            if (!_bookingService.IsValidApartment(bookingDTO.ApartmentId))
                {
                return BadRequest("Invalid apartment.");
                }

            if (!_bookingService.IsValidPackage(bookingDTO.ApartmentId, bookingDTO.PackageId))
                {
                return BadRequest("Selected package is not available for this apartment.");
                }

            if (!_bookingService.AreValidAddOns(bookingDTO.ApartmentId, bookingDTO.AddOnIds))
                {
                return BadRequest("Some of the selected add-ons are not available for this apartment.");
                }

            // Proceed with booking
            _bookingService.AddBooking(bookingDTO);
            return CreatedAtAction(nameof(GetBookingById), new { id = bookingDTO.BookingId }, bookingDTO);
            }

        [HttpPut("{id}")]
        public IActionResult UpdateBooking(Guid id, [FromBody] BookingDTO bookingDTO)
            {
            try
                {
                _bookingService.UpdateBooking(id, bookingDTO);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(Guid id)
            {
            try
                {
                _bookingService.DeleteBooking(id);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost("{id}/finalize")]
        public IActionResult FinalizeBooking(Guid id)
            {
            try
                {
                _bookingService.FinalizeBooking(id);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }
        }
    }
