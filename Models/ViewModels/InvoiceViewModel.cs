using System;

namespace OdaMeClone.Models.ViewModels
    {
    public class InvoiceViewModel
        {
        public Guid InvoiceId { get; set; }
        public string CustomerName { get; set; } // Display customer name
        public string ApartmentName { get; set; } // Display apartment name
        public string BookingDetails { get; set; } // Display booking details
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string InvoiceStatus { get; set; } // Display invoice status as string
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentStatus { get; set; } // Display payment status as string
        public string DisplayAmount => $"{Amount:C}";
        public string DisplayDueDate => DueDate.ToString("d");
        }
    }
