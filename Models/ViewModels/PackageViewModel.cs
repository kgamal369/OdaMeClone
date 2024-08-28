using System;

namespace OdaMeClone.Models.ViewModels
{
    public class PackageViewModel
    {
        public Guid PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackageTypeName { get; set; } // Display package type as a string
        public decimal Price { get; set; }
        public string DisplayPrice => $"{Price:C}";
    }
}
