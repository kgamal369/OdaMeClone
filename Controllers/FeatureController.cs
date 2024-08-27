using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Data;
using OdaMeClone.Models;
using Microsoft.EntityFrameworkCore;

namespace OdaMeClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly OdaDbContext _context;

        public FeatureController(OdaDbContext context)
        {
            _context = context;
        }

        // GET: api/Feature
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeatures()
        {
            return await _context.Features.Include(f => f.Apartment).ToListAsync();
        }

        // GET: api/Feature/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feature>> GetFeature(int id)
        {
            var feature = await _context.Features.Include(f => f.Apartment).FirstOrDefaultAsync(f => f.Id == id);

            if (feature == null)
            {
                return NotFound();
            }

            return feature;
        }

        // PUT: api/Feature/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeature(int id, Feature feature)
        {
            if (id != feature.Id)
            {
                return BadRequest();
            }

            _context.Entry(feature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeatureExists(id))
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

        // POST: api/Feature
        [HttpPost]
        public async Task<ActionResult<Feature>> PostFeature(Feature feature)
        {
            _context.Features.Add(feature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeature", new { id = feature.Id }, feature);
        }

        // DELETE: api/Feature/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var feature = await _context.Features.FindAsync(id);
            if (feature == null)
            {
                return NotFound();
            }

            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeatureExists(int id)
        {
            return _context.Features.Any(e => e.Id == id);
        }
    }
}
