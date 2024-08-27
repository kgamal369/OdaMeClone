namespace OdaMeClone.Dtos.Projects
    {
    public class InvoiceDto
        {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int ApartmentId { get; set; }
        public int CustomerId { get; set; }
        }
    }
