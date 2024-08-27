using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Data;
using OdaMeClone.Models;
using Microsoft.EntityFrameworkCore;

namespace OdaMeClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly OdaDbContext _context;

        public TaskController(OdaDbContext context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OdaMeClone.Models.Task>>> GetTasks()
        {
            return await _context.Tasks.Include(t => t.Project).Include(t => t.Resource).Include(t => t.Feature).Include(t => t.Apartment).Include(t => t.Invoice).ToListAsync();
        }

        // GET: api/Task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OdaMeClone.Models.Task>> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Resource)
                .Include(t => t.Feature)
                .Include(t => t.Apartment)
                .Include(t => t.Invoice)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, OdaMeClone.Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // POST: api/Task
        [HttpPost]
        public async Task<ActionResult<OdaMeClone.Models.Task>> PostTask(OdaMeClone.Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
