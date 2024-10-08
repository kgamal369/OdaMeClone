using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OdaMeClone.Data;

namespace OdaMeClone.Models
    {
    public class AddOn
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AddOnId { get; set; } // Primary Key

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "AddOn Name must be between 2 and 100 characters.")]
        public string? AddOnName { get; set; } // Name of the addon

        [Required]
        public AddOnType AddOnType { get; set; } // Enum for AddOn Type

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1000000, ErrorMessage = "Price per unit must be a positive value.")]
        public decimal PricePerUnit { get; set; } // Price per unit

        [Required]
        [Range(1, 100, ErrorMessage = "Max units must be between 1 and 100.")]
        public int MaxUnits { get; set; } // Maximum units allowed for installation

        // Navigation property for the many-to-many relationship via ApartmentAddOn
        public virtual ICollection<ApartmentAddOn> ApartmentAddOns { get; set; }

        [Range(0, 100, ErrorMessage = "Installed units must be between 0 and the maximum units allowed.")]
        public int InstalledUnits { get; set; } // Actual installed units

        public AddOn()
            {
            ApartmentAddOns = new List<ApartmentAddOn>();
            }

        // Method to update the price of an addon and propagate the change to affected apartments
        public void UpdateAddOnPrice(OdaDbContext context, decimal newPrice)
            {
            PricePerUnit = newPrice;

            // Fetch all apartments that have this addon assigned
            var apartments = context.Apartments
                  .Where(a => a.AssignedApartmentAddOns.Any(ad => ad.AddOnId == AddOnId))
                  .ToList();

            foreach (var apartment in apartments)
                {
                apartment.CalculateTotalPrice();
                }

            context.SaveChanges();
            }

        // Method to validate the number of installed units
        public void ValidateInstalledUnits()
            {
            if (InstalledUnits > MaxUnits)
                {
                throw new InvalidOperationException($"Installed units for {AddOnName} cannot exceed the maximum limit of {MaxUnits} units.");
                }
            }
        }

    public class ApartmentAddOn
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The DB will auto-generate the Guid
        public Guid ApartmentAddOnId { get; set; }
        public Guid ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }
        public Guid AddOnId { get; set; }
        public AddOn? AddOn { get; set; }
        public int InstalledUnits { get; set; } // Additional property for installed units
        }

    }
