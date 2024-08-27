using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OdaMeClone.Controllers
    {
    [ApiController]
    public abstract class BaseController : ControllerBase
        {
        protected readonly ILogger<BaseController> Logger;

        protected BaseController(ILogger<BaseController> logger)
            {
            Logger = logger;
            }

        protected IActionResult HandleError(string message)
            {
            Logger.LogError(message);
            return BadRequest(new { error = message });
            }

        protected IActionResult HandleSuccess(object result)
            {
            return Ok(result);
            }

        protected IActionResult HandleNotFound(string entityName)
            {
            Logger.LogWarning($"{entityName} not found.");
            return NotFound(new { error = $"{entityName} not found." });
            }

        protected IActionResult HandleException(Exception ex)
            {
            Logger.LogError(ex, ex.Message);
            return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }
    }
