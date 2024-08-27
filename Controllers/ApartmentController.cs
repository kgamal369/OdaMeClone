using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
        {
        private readonly OdaDbContext _context;

        public ApartmentController(OdaDbContext context)
            {
            _context = context;
            }

        // GET: api/Apartment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartments()
            {
            return await _context.Apartments.Include(a => a.Features).Include(a => a.Tasks).Include(a => a.Invoices).ToListAsync();
            }

        // GET: api/Apartment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Apartment>> GetApartment(int id)
            {
            var apartment = await _context.Apartments
                                          .Include(a => a.Features)
                                          .Include(a => a.Tasks)
                                          .Include(a => a.Invoices)
                                          .FirstOrDefaultAsync(a => a.Id == id);

            if (apartment == null)
                {
                return NotFound();
                }

            return apartment;
            }

        // PUT: api/Apartment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(int id, Apartment apartment)
            {
            if (id != apartment.Id)
                {
                return BadRequest();
                }

            _context.Entry(apartment).State = EntityState.Modified;

            try
                {
                await _context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!ApartmentExists(id))
                    {
                    return NotFound();
                    }
                else
                    {
                    throw;
                    }
                }

            return NoContent();
            }

        // POST: api/Apartment
        [HttpPost]
        public async Task<ActionResult<Apartment>> PostApartment(Apartment apartment)
            {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApartment", new { id = apartment.Id }, apartment);
            }

        // DELETE: api/Apartment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
            {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
                {
                return NotFound();
                }

            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();

            return NoContent();
            }

        private bool ApartmentExists(int id)
            {
            return _context.Apartments.Any(e => e.Id == id);
            }
        }
    }
