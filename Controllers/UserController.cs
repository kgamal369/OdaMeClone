using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
        {
        private readonly UserService _userService;

        public UserController(UserService userService)
            {
            _userService = userService;
            }

        [HttpGet]
        public IActionResult GetAllUsers()
            {
            var users = _userService.GetAllUsers();
            return Ok(users);
            }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
            {
            try
                {
                var user = _userService.GetUserById(id);
                return Ok(user);
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserDTO userDTO, string passwordHash)
            {
            if (userDTO == null)
                {
                return BadRequest("Invalid user data.");
                }

            _userService.AddUser(userDTO, passwordHash);
            return CreatedAtAction(nameof(GetUserById), new { id = userDTO.Id }, userDTO);
            }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
            {
            try
                {
                _userService.UpdateUser(id, userDTO);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
            {
            try
                {
                _userService.DeleteUser(id);
                return NoContent();
                }
            catch (KeyNotFoundException ex)
                {
                return NotFound(ex.Message);
                }
            }
        }
    }
