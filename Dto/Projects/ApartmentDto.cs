using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class ApartmentDto
        {
        public int Id { get; set; }
        public double Space { get; set; }
        public List<FeatureDto> Features { get; set; }
        public string Status { get; set; }
        //        public LocationFactorsDto LocationFactors { get; set; }
        public decimal Price { get; set; }
        public int CustomerId { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<InvoiceDto> Invoices { get; set; }
        }
    }
