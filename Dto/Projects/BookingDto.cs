using System;
using System.Collections.Generic;
using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class BookingDTO
        {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid PackageId { get; set; }  // Add this property
        public List<Guid> AddOnIds { get; set; }  // Add this property
        public DateTime CreatedDateTime { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public string? AssignedPerson { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<InvoiceDTO>? Invoices { get; set; }
        }
    }
