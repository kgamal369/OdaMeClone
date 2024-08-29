using System;

namespace OdaMeClone.Models.ViewModels
    {
    public class PaymentViewModel
        {
        public Guid PaymentId { get; set; }
        public string CustomerName { get; set; } // Display customer name
        public string InvoiceNumber { get; set; } // Display invoice number or details
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // Display payment method as a string

        public string PaymentSummary => $"{AmountPaid:C} paid on {PaymentDate.ToShortDateString()} via {PaymentMethod}";
        }
    }
