using System;
using System.Collections.Generic;

namespace OdaMeClone.Models.ViewModels
    {
    public class ApartmentViewModel
        {
        public Guid ApartmentId { get; set; }
        public string? ApartmentName { get; set; }
        public string ApartmentType { get; set; }

        public string? ProjectName { get; set; } // Display the project name
        public double Space { get; set; }
        public string? ViewType { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<byte[]>? ApartmentPhotos { get; set; }
        public string? AvailabilityStatus { get; set; } // e.g., "Available" or "Booked"

        public string DisplayPrice => TotalPrice.HasValue ? $"{TotalPrice:C}" : "Price not available";
        }
    }
