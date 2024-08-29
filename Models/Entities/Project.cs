using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
    {
    public class Project
        {
        [Key]
        public Guid ProjectId { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; } // Name of the project

        [Required]
        [StringLength(200)]
        public string Location { get; set; } // Location of the project

        [StringLength(1000)]
        public string Amenities { get; set; } // Description of common amenities

        [Required]
        public int TotalUnits { get; set; } // Total number of apartments in the project

        public byte[] ProjectLogo { get; set; } // Project logo or photo

        public virtual ICollection<Apartment> Apartments { get; set; } // List of associated apartments

        public Project()
            {
            Apartments = new List<Apartment>();
            }

        // Method to add an apartment to the project
        public void AddApartment(Apartment apartment)
            {
            if (Apartments == null)
                {
                Apartments = new List<Apartment>();
                }

            Apartments.Add(apartment);
            }

        // Method to remove an apartment from the project
        public void RemoveApartment(Apartment apartment)
            {
            Apartments?.Remove(apartment);
            }

        // Method to get a summary of the project
        public string GetProjectSummary()
            {
            return $"{ProjectName} located at {Location} with {TotalUnits} total units.";
            }
        }
    }
