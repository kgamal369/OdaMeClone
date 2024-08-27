using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
        {
        private readonly OdaDbContext _context;

        public ResourceController(OdaDbContext context)
            {
            _context = context;
            }

        // GET: api/Resource
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
            {
            return await _context.Resources.Include(r => r.Project).ToListAsync();
            }

        // GET: api/Resource/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(int id)
            {
            var resource = await _context.Resources.Include(r => r.Project).FirstOrDefaultAsync(r => r.Id == id);

            if (resource == null)
                {
                return NotFound();
                }

            return resource;
            }

        // PUT: api/Resource/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResource(int id, Resource resource)
            {
            if (id != resource.Id)
                {
                return BadRequest();
                }

            _context.Entry(resource).State = EntityState.Modified;

            try
                {
                await _context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!ResourceExists(id))
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

        // POST: api/Resource
        [HttpPost]
        public async Task<ActionResult<Resource>> PostResource(Resource resource)
            {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResource", new { id = resource.Id }, resource);
            }

        // DELETE: api/Resource/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
            {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
                {
                return NotFound();
                }

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return NoContent();
            }

        private bool ResourceExists(int id)
            {
            return _context.Resources.Any(e => e.Id == id);
            }
        }
    }
