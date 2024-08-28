using System;
using Microsoft.AspNetCore.Mvc;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly PackageService _packageService;

        public PackageController(PackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet]
        public IActionResult GetAllPackages()
        {
            var packages = _packageService.GetAllPackages();
            return Ok(packages);
        }

        [HttpGet("{id}")]
        public IActionResult GetPackageById(Guid id)
        {
            try
            {
                var package = _packageService.GetPackageById(id);
                return Ok(package);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddPackage([FromBody] PackageDTO packageDTO)
        {
            if (packageDTO == null)
            {
                return BadRequest("Invalid package data.");
            }

            _packageService.AddPackage(packageDTO);
            return CreatedAtAction(nameof(GetPackageById), new { id = packageDTO.PackageId }, packageDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePackage(Guid id, [FromBody] PackageDTO packageDTO)
        {
            try
            {
                _packageService.UpdatePackage(id, packageDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePackage(Guid id)
        {
            try
            {
                _packageService.DeletePackage(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/updateprice")]
        public IActionResult UpdatePackagePrice(Guid id, [FromBody] decimal newPrice)
        {
            try
            {
                _packageService.UpdatePackagePrice(id, newPrice);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
