using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdaMeClone.Dtos.Projects
    {
    public class ProjectDTO
        {
        public Guid ProjectId { get; set; } // Project ID (optional in POST request, will be auto-generated if not provided)

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "Project name can't be longer than 100 characters.")]
        public string? ProjectName { get; set; } // Name of the project, required field

        [StringLength(200, ErrorMessage = "Location can't be longer than 200 characters.")]
        public string? Location { get; set; } // Optional location field

        [StringLength(1000, ErrorMessage = "Amenities can't be longer than 1000 characters.")]
        public string? Amenities { get; set; } // Optional description of amenities

        public int TotalUnits { get; set; } // Total units, auto-calculated (can be ignored during POST/PUT)

        [Required(ErrorMessage = "Project logo is required.")]
        public IFormFile? ProjectLogo { get; set; } // The project logo in byte array (Base64 encoded image in the request)

        [Required(ErrorMessage = "At least one apartment ID is required.")]
        public List<Guid>? ApartmentIds { get; set; } // List of associated apartment IDs
        }
    }
