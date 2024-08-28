using System;
using System.Collections.Generic;

namespace OdaMeClone.Dtos.Projects
{
    public class ProjectDTO
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Location { get; set; }
        public string Amenities { get; set; }
        public int TotalUnits { get; set; }
        public byte[] ProjectLogo { get; set; }
        public List<Guid> ApartmentIds { get; set; } // List of associated apartment IDs
    }
}
