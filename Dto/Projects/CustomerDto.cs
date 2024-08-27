namespace OdaMeClone.Dtos.Projects
    {
    public class CustomerDto
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactDetails { get; set; }
        public List<ApartmentDto> Apartments { get; set; }
        public List<InvoiceDto> Invoices { get; set; }
        }
    }
