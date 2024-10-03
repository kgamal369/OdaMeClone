using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
    {
    public class Apartment
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ApartmentId { get; set; } // Primary Key

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Apartment Name must be between 2 and 100 characters.")]
        public string? ApartmentName { get; set; } // Name of the apartment

        [Required]
        [EnumDataType(typeof(ApartmentType), ErrorMessage = "Invalid Apartment Type.")]
        public ApartmentType ApartmentType { get; set; } // Enum for Apartment Type
                                                         // Status of the Apartment
        [Required]
        [EnumDataType(typeof(ApartmentStatus), ErrorMessage = "Invalid Apartment Status.")]
        public ApartmentStatus ApartmentStatus { get; set; } // Enum to track apartment status

        [Required]
        [Range(10, 1000, ErrorMessage = "Space must be between 10 and 1,000 square meters.")]
        public double Space { get; set; }  // Space in square meters

        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string? Description { get; set; } // Description of the apartment

        public List<byte[]>? ApartmentPhotos { get; set; }  // List of photos of the apartment

        // Navigation properties to ensure proper relationship mapping
        [Required]
        public Guid ProjectId { get; set; }  // Foreign Key to Project
        public virtual Project Project { get; set; } = null!;// Navigation property to Project

        // Conditional attributes
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; } // Foreign Key to Customer, nullable if not booked
        public virtual Customer? Customer { get; set; } // Navigation property to Customer
        public ICollection<User> Users { get; set; } = [];

        public virtual ICollection<Package> AvailablePackages { get; set; } = []; // List of associated available packages

        [ForeignKey("AssignedPackage")]
        public Guid? AssignedPackageId { get; set; } // Foreign Key to Assigned Package
        public virtual Package AssignedPackage { get; set; } // Navigation Property
        public virtual ICollection<ApartmentAddOn> AvailableApartmentAddOns { get; set; } = [];

        // Navigation property for many-to-many relationship with AddOns via ApartmentAddOn
        public virtual ICollection<ApartmentAddOn> AssignedApartmentAddOns { get; set; } = [];// List of selected addons (Optional)

        // Calculated field for total price
        [NotMapped]
        public decimal? TotalPrice => CalculateTotalPrice();
        // Additional fields for more detailed apartment attributes
        public int FloorNumber { get; set; } // Floor number of the apartment
        public string ViewType { get; set; } = string.Empty; // e.g., Sea View, Garden View
        public DateTime? AvailabilityDate { get; set; } // Date when the apartment becomes available
        // Constructor initializing collections
        // public Apartment(Guid projectId, double space, Project project )
        //     {
        //     ProjectId = projectId;
        //     Project = project;
        //     ApartmentStatus = ApartmentStatus.ForSale;
        //     Space = space;
        //     }
        //  public static Apartment Create(Guid projectId, double space, Project project)
        // {
        //     if (double.IsNaN(space)) throw new ArgumentException("Space is required.");
        //     if (project == null) throw new ArgumentNullException(nameof(project));

        //     return new Apartment(projectId, space, project);
        // }

        public decimal CalculateTotalPrice()
            {
            // Apply discounts, taxes, or other adjustments here if needed
            // Calculate based on the related ApartmentAddOns
            return (AssignedPackage?.Price ?? 0) +
             (AvailableApartmentAddOns?.Sum(a => a.AddOn.PricePerUnit * a.InstalledUnits) ?? 0);
            }
        // Method to add a package
        public void AddPackage(Package package)
            {
            if (AvailablePackages == null)
                AvailablePackages = [];

            AvailablePackages.Add(package);
            }

        // Method to remove a package
        public void RemovePackage(Package package)
            {
            AvailablePackages?.Remove(package);
            }
        public bool ValidatePackageStatus()
            {
            if (ApartmentStatus == ApartmentStatus.Booked ||
                ApartmentStatus == ApartmentStatus.InProgress ||
                ApartmentStatus == ApartmentStatus.InReview)
                {
                if (AssignedPackage == null)
                    {
                    throw new InvalidOperationException("An apartment must have an assigned package when the status is Booked, In Progress, or In Review.");
                    }
                }
            return true;
            }

        public bool ValidateAddons()
            {
            foreach (var addon in AssignedApartmentAddOns)
                {
                if (addon.InstalledUnits > addon.AddOn.MaxUnits) // AddOn property contains MaxUnits
                    {
                    throw new InvalidOperationException($"Addon {addon.AddOn.AddOnName} exceeds the maximum allowed units.");
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

            if (AssignedApartmentAddOns != null)
                {
                foreach (var addon in AssignedApartmentAddOns)
                    {
                    if (addonPrices.ContainsKey(addon.AddOnId))
                        {
                        addon.AddOn.PricePerUnit = addonPrices[addon.AddOnId];
                        }
                    }
                }
            }

        public void UpdateApartmentPriceAndValidate()
            {
            ValidatePackageStatus();
            ValidateAddons();
            }
        }
    }

