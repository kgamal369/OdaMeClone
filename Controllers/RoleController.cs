using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
        {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
            {
            _roleService = roleService;
            }

        [HttpGet]
        public IActionResult GetAllRoles()
            {
            var roles = _roleService.GetAllRoles();
            return Ok(roles);
            }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
            {
            try
                {
                var role = _roleService.GetRoleById(id);
                return Ok(role);
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public IActionResult AddRole([FromBody] RoleDTO roleDTO)
            {
            if (roleDTO == null)
                {
                return BadRequest("Invalid role data.");
                }

            _roleService.AddRole(roleDTO);
            return CreatedAtAction(nameof(GetRoleById), new { id = roleDTO.Id }, roleDTO);
            }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] RoleDTO roleDTO)
            {
            try
                {
                _roleService.UpdateRole(id, roleDTO);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
            {
            try
                {
                _roleService.DeleteRole(id);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }
        }
    }
