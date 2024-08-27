using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
    {
    public class Project
        {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; } // Location influences price

        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

        public ICollection<Resource> Resources { get; set; } = new List<Resource>();

        public ICollection<Task> Tasks { get; set; } = new List<Task>();  // Add this line to include the Tasks collection

        public double LocationFactor { get; set; } // A dynamic multiplier for location

        public double CalculateLocationMultiplier()
            {
            // Example logic for calculating a location multiplier based on the Location string
            switch (Location.ToLower())
                {
                case "prime location":
                    return 1.5;
                case "good view":
                    return 1.2;
                default:
                    return 1.0;
                }
            }

        [Required]
        public string Creator { get; set; } // Assuming Creator is a string representing the name of the person who created the project
        }
    }
