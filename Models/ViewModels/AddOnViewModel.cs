using System;

namespace OdaMeClone.Models.ViewModels
{
    public class AddOnViewModel
    {
        public Guid AddOnId { get; set; }
        public string AddOnName { get; set; }
        public string AddOnTypeName { get; set; } // To display a human-readable type name
        public decimal PricePerUnit { get; set; }
        public int MaxUnits { get; set; }
        public int InstalledUnits { get; set; }
        public string DisplayPrice => $"{PricePerUnit:C}";
    }
}
