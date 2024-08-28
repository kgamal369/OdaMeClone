using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OdaMeClone.Models
{
    public class Apartment
    {
        [Key]
        public Guid ApartmentId { get; set; } // Primary Key

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Apartment Name must be between 2 and 100 characters.")]
        public string ApartmentName { get; set; } // Name of the apartment

        [Required]
        public ApartmentType ApartmentType { get; set; } // Enum for Apartment Type

        [Required]
        [Range(10, 10000, ErrorMessage = "Space must be between 10 and 10,000 square meters.")]
        public double Space { get; set; } // Space in square meters

        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string Description { get; set; } // Description of the apartment

        public List<byte[]> ApartmentPhotos { get; set; } // List of photos of the apartment

        public virtual ICollection<Package> PackagesList { get; set; } // List of associated packages

        public virtual ICollection<AddOn> AddonsList { get; set; } // List of associated addons

        // Conditional attributes
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; } // Foreign Key to Customer, nullable if not booked
        public virtual Customer Customer { get; set; } // Navigation property to Customer

        // Status of the Apartment
        [Required]
        public ApartmentStatus Status { get; set; } // Enum to track apartment status

        // Navigation properties to ensure proper relationship mapping
        public Guid ProjectId { get; set; }  // Foreign Key to Project
        public virtual Project Project { get; set; } // Navigation property to Project

        [ForeignKey("AssignedPackage")]
        public Guid? AssignedPackageId { get; set; } // Foreign Key to Assigned Package
        public virtual Package AssignedPackage { get; set; } // Navigation Property

        public virtual ICollection<AddOn> AssignedAddons { get; set; } // List of selected addons (Optional)

        // Calculated field for total price
        [NotMapped]
        public decimal? TotalPrice => CalculateTotalPrice();

        // Additional fields for more detailed apartment attributes
        public int FloorNumber { get; set; } // Floor number of the apartment
        public string ViewType { get; set; } // e.g., Sea View, Garden View
        public DateTime? AvailabilityDate { get; set; } // Date when the apartment becomes available

        // Constructor initializing collections
        public Apartment()
        {
            ApartmentPhotos = new List<byte[]>();
            PackagesList = new List<Package>();
            AddonsList = new List<AddOn>();
            AssignedAddons = new List<AddOn>();
        }

        // Method to add a package
        public void AddPackage(Package package)
        {
            if (PackagesList == null)
                PackagesList = new List<Package>();

            PackagesList.Add(package);
        }

        // Method to remove a package
        public void RemovePackage(Package package)
        {
            PackagesList?.Remove(package);
        }

        // Method to calculate the total price
        public decimal CalculateTotalPrice()
        {
            // Apply discounts, taxes, or other adjustments here if needed
            return (AssignedPackage?.Price ?? 0) + (AssignedAddons?.Sum(a => a.PricePerUnit * a.InstalledUnits) ?? 0);
        }

        // Method to validate addon selections
        public bool ValidateAddons()
        {
            foreach (var addon in AssignedAddons)
            {
                if (addon.InstalledUnits > addon.MaxUnits)
                {
                    throw new InvalidOperationException($"Addon {addon.AddOnName} exceeds the maximum allowed units.");
                }
            }
            return true;
        }

        // Method to propagate price updates throughout the system
        public void UpdatePrices(decimal newPackagePrice, Dictionary<Guid, decimal> addonPrices)
        {
            if (AssignedPackage != null)
            {
                AssignedPackage.Price = newPackagePrice;
            }

            if (AssignedAddons != null)
            {
                foreach (var addon in AssignedAddons)
                {
                    if (addonPrices.ContainsKey(addon.AddOnId))
                    {
                        addon.PricePerUnit = addonPrices[addon.AddOnId];
                    }
                }
            }
        }
    }




}

