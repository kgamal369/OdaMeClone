using System;
using System.Collections.Generic;

namespace OdaMeClone.Models.ViewModels
{
    public class ProjectViewModel
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Location { get; set; }
        public string Amenities { get; set; }
        public int TotalUnits { get; set; }
        public byte[] ProjectLogo { get; set; }
        public List<string> ApartmentNames { get; set; } // Display names of associated apartments

        public string ProjectSummary => $"{ProjectName} located at {Location} with {TotalUnits} total units.";
    }
}
