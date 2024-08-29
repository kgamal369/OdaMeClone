using System;
using System.Collections.Generic;

namespace OdaMeClone.Models.ViewModels
    {
    public class BookingViewModel
        {
        public Guid BookingId { get; set; }
        public string CustomerName { get; set; } // Display customer name
        public string ApartmentName { get; set; } // Display apartment name
        public DateTime CreatedDateTime { get; set; }
        public BookingStatus Status { get; set; }
        public string AssignedPerson { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public string PaymentMethodName { get; set; } // Display payment method name
        public List<InvoiceViewModel> Invoices { get; set; }

        public string DisplayStatus => Status.ToString();
        }
    }
