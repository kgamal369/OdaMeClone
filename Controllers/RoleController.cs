using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/Roles")]
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
        public IActionResult AddRole([FromBody] Role roleDTO)
            {
            if (roleDTO == null)
                {
                return BadRequest("Invalid role data.");
                }

            _roleService.AddRole(roleDTO);
            return CreatedAtAction(nameof(GetRoleById), new { id = roleDTO.RoleId }, roleDTO);
            }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] Role roleDTO)
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
