namespace OdaMeClone.Dtos.Projects
    {
    public class PaymentDto
        {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public int InvoiceId { get; set; }
        }
    }
