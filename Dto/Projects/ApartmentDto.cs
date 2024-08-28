using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
     public class ApartmentDTO
    {
        public Guid ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public double Space { get; set; }
        public string Description { get; set; }
        public List<byte[]> ApartmentPhotos { get; set; }
        public int FloorNumber { get; set; }
        public string ViewType { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        public decimal? TotalPrice { get; set; }

        public Guid ProjectId { get; set; } // Assuming you want to show the project it belongs to
    }
    }
