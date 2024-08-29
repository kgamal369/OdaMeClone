using System;
using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class PaymentDTO
        {
        public Guid PaymentId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        }
    }
