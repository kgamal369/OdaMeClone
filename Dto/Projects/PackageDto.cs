using System;
using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class PackageDTO
        {
        public Guid PackageId { get; set; }
        public string? PackageName { get; set; }
        public PackageType PackageType { get; set; }
        public decimal Price { get; set; }
        }
    }
