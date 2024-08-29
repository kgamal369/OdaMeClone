using System;

namespace OdaMeClone.Dtos.Projects
    {
    public class ApartmentAddOnDTO
        {
        public Guid ApartmentId { get; set; }
        public Guid AddOnId { get; set; }
        public int InstalledUnits { get; set; }
        }
    }
