using System;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OdaMeClone.Data;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/Projects")]
    [ApiController]
    public class ProjectController : ControllerBase
        {
        private readonly ProjectService _projectService;
        private readonly OdaDbContext _context; // Declare OdaDbContext
        private readonly ApartmentService _apartmentService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ProjectService projectService, ApartmentService apartmentService, ILogger<ProjectController> logger, OdaDbContext context)
            {
            _projectService = projectService;
            _apartmentService = apartmentService;
            _logger = logger;
            _context = context;
            }

        [HttpGet]
        public IActionResult GetAllProjects()
            {
            var projects = _projectService.GetAllProjects();
            return Ok(projects);
            }

        [HttpGet("{id}")]
        public IActionResult GetProjectById(Guid id)
            {
            try
                {
                var project = _projectService.GetProjectById(id);
                return Ok(project);
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public IActionResult AddProject(Project projectDTO)
            {
            if (projectDTO == null)
                {
                return BadRequest("Invalid project data.");
                }

            byte[] logoBytes = null;
            if (projectDTO.ProjectLogo != null)
                {
                using (var memoryStream = new MemoryStream())
                    {
                    //projectDTO.ProjectLogo.CopyTo(memoryStream);
                    logoBytes = memoryStream.ToArray();
                    }
                }

            // Check if the provided ApartmentIds exist in the database
            if (projectDTO.Apartments != null && projectDTO.Apartments.Count > 0)
                {
                var existingApartmentIds = _context.Apartments.Select(a => a.ApartmentId).ToList();
                // Select the ApartmentIds from projectDTO.Apartments and apply Except
                var nonExistingIds = projectDTO.Apartments
                    .Select(a => a.ApartmentId) // Extract ApartmentId from Apartment objects
                    .Except(existingApartmentIds) // Compare ApartmentIds
                    .ToList();

                if (nonExistingIds.Any())
                    {
                    return BadRequest($"The following Apartment IDs do not exist: {string.Join(", ", nonExistingIds)}");
                    }
                }

            _projectService.AddProject(projectDTO, logoBytes);
            return CreatedAtAction(nameof(GetProjectById), new { id = projectDTO.ProjectId }, projectDTO);
            }


        [HttpPost("assign-apartment-to-project")]
        public async Task<IActionResult> AssignedApartmentToProject([FromBody] Guid apartmentId, [FromBody] Guid projectId)
            {
            try
                {
                var projectEntity = _projectService.GetProjectEntityById(projectId);
                if (projectEntity == null)
                    {
                    return NotFound("Project not found.");
                    }

                var apartment = _apartmentService.GetApartmentById(apartmentId);
                if (apartment == null)
                    {
                    return NotFound("Apartment not found.");
                    }

                projectEntity.AddApartment(apartment);
                _projectService.UpdateProject(projectEntity);

                return Ok("Apartment successfully assigned to project.");
                }
            catch (Exception ex)
                {
                _logger.LogError(ex, "Error while assigning apartment to project.");
                return StatusCode(500, "An error occurred while processing your request.");
                }
            }


        [HttpPost("deassign-apartment-to-project")]
        public async Task<IActionResult> DeAssignedApartmentToProject([FromBody] Guid apartmentId, [FromBody] Guid projectId)
            {
            try
                {
                var projectEntity = _projectService.GetProjectEntityById(projectId);
                if (projectEntity == null)
                    {
                    return NotFound("Project not found.");
                    }

                var apartment = _apartmentService.GetApartmentById(apartmentId);
                if (apartment == null)
                    {
                    return NotFound("Apartment not found.");
                    }

                projectEntity.RemoveApartment(apartment);
                _projectService.UpdateProject(projectEntity);

                return Ok("Apartment successfully assigned to project.");
                }
            catch (Exception ex)
                {
                _logger.LogError(ex, "Error while assigning apartment to project.");
                return StatusCode(500, "An error occurred while processing your request.");
                }
            }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, Project projectDTO)
            {
            if (projectDTO == null)
                {
                return BadRequest("Invalid project data.");
                }

            byte[] logoBytes = null;
            if (projectDTO.ProjectLogo != null)
                {
                using (var memoryStream = new MemoryStream())
                    {
                    //await projectDTO.ProjectLogo.CopyToAsync(memoryStream); // Correct usage with IFormFile
                    logoBytes = memoryStream.ToArray(); // Convert to byte array
                    }
                }

            _projectService.UpdateProject(id, projectDTO, logoBytes);
            return NoContent();
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(Guid id)
            {
            try
                {
                _projectService.DeleteProject(id);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }
        }
    }
