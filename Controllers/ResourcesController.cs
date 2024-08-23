[ApiController]
[Route("api/[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ResourcesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllResources()
    {
        var resources = await _context.Resources.Include(r => r.Project)
                                                .ToListAsync();
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetResourceById(int id)
    {
        var resource = await _context.Resources.Include(r => r.Project)
                                               .SingleOrDefaultAsync(r => r.Id == id);

        if (resource == null)
        {
            return NotFound();
        }

        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateResource([FromBody] Resource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Resources.Add(resource);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetResourceById), new { id = resource.Id }, resource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateResource(int id, [FromBody] Resource resource)
    {
        if (id != resource.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
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
