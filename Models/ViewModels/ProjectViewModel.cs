using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OdaMeClone.ViewModels
    {
    public class ProjectViewModel
        {
        public Guid ProjectId { get; set; } // Project ID (can be optional for the view in some cases)

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "Project name can't be longer than 100 characters.")]
        public string? ProjectName { get; set; } // Project name

        [StringLength(200, ErrorMessage = "Location can't be longer than 200 characters.")]
        public string? Location { get; set; } // Project location (optional)

        [StringLength(1000, ErrorMessage = "Amenities can't be longer than 1000 characters.")]
        public string? Amenities { get; set; } // Amenities (optional)

        public int TotalUnits { get; set; } // Total number of units (auto-calculated, read-only)

        [Required(ErrorMessage = "Project logo is required.")]
        public IFormFile? ProjectLogo { get; set; } // File upload for logo (IFormFile for form handling)

        [Required(ErrorMessage = "At least one apartment ID is required.")]
        public List<Guid>? ApartmentIds { get; set; } // List of associated apartment IDs
        }
    }
