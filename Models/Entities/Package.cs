using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OdaMeClone.Data;

namespace OdaMeClone.Models
    {
    public class Package
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PackageId { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string PackageName { get; set; } // Name of the package

        [Required]
        public PackageType PackageType { get; set; } // Enum for Package Type

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 100000000, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; } // Price of the package

        // Method to update package price and cascade the change
        public void UpdatePackagePrice(OdaDbContext context, decimal newPrice)
            {
            Price = newPrice;

            // Fetch all apartments that have this package assigned
            var apartments = context.Apartments
                .Where(a => a.AssignedPackageId == this.PackageId)
                .ToList();

            // Recalculate total price for each apartment
            foreach (var apartment in apartments)
                {
                apartment.CalculateTotalPrice();
                }

            // Save changes to the database
            context.SaveChanges();
            }
        }

    }
