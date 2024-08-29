using System;
using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class InvoiceDTO
        {
        public Guid InvoiceId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public InvoiceStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        }
    }
