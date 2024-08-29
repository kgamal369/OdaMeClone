using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
        {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
            {
            _projectService = projectService;
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
        public IActionResult AddProject([FromBody] ProjectDTO projectDTO)
            {
            if (projectDTO == null)
                {
                return BadRequest("Invalid project data.");
                }

            _projectService.AddProject(projectDTO);
            return CreatedAtAction(nameof(GetProjectById), new { id = projectDTO.ProjectId }, projectDTO);
            }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(Guid id, [FromBody] ProjectDTO projectDTO)
            {
            try
                {
                _projectService.UpdateProject(id, projectDTO);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
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
