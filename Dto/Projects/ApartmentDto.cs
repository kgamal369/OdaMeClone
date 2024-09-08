using System;
using System.Collections.Generic;
using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class ApartmentDTO
        {
        public Guid ApartmentId { get; set; }
        public string? ApartmentName { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public double Space { get; set; }
        public string? Description { get; set; }
        public List<byte[]>? ApartmentPhotos { get; set; }
        public List<Guid>? PackageIds { get; set; }
        public List<Guid>? AddOnIds { get; set; }
        public Guid? CustomerId { get; set; }
        public ApartmentStatus ApartmentStatus { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? AssignedPackageId { get; set; }
        public List<ApartmentAddOnDTO>? ApartmentAddOns { get; set; }
        public int FloorNumber { get; set; }
        public string? ViewType { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        public decimal? TotalPrice { get; set; }
        }
    }
