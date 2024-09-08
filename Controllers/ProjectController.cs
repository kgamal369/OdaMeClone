using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Data;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
        {
        private readonly ProjectService _projectService;
        private readonly OdaDbContext _context; // Declare OdaDbContext


        public ProjectController(ProjectService projectService, OdaDbContext context)
            {
            _projectService = projectService;
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
        public IActionResult AddProject([FromForm] ProjectDTO projectDTO)
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
                    projectDTO.ProjectLogo.CopyTo(memoryStream);
                    logoBytes = memoryStream.ToArray();
                    }
                }

            // Check if the provided ApartmentIds exist in the database
            if (projectDTO.ApartmentIds != null && projectDTO.ApartmentIds.Count > 0)
                {
                var existingApartmentIds = _context.Apartments.Select(a => a.ApartmentId).ToList();
                var nonExistingIds = projectDTO.ApartmentIds.Except(existingApartmentIds).ToList();

                if (nonExistingIds.Any())
                    {
                    return BadRequest($"The following Apartment IDs do not exist: {string.Join(", ", nonExistingIds)}");
                    }
                }

            _projectService.AddProject(projectDTO, logoBytes);
            return CreatedAtAction(nameof(GetProjectById), new { id = projectDTO.ProjectId }, projectDTO);
            }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(Guid id, [FromForm] ProjectDTO projectDTO)
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
                    projectDTO.ProjectLogo.CopyTo(memoryStream);
                    logoBytes = memoryStream.ToArray();
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
