using System;
using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class AddOnDTO
        {
        public Guid AddOnId { get; set; }
        public string AddOnName { get; set; }
        public AddOnType AddOnType { get; set; }
        public decimal PricePerUnit { get; set; }
        public int MaxUnits { get; set; }
        public int InstalledUnits { get; set; }
        }
    }
