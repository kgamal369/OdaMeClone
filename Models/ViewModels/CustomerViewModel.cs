using System;
using System.Collections.Generic;

namespace OdaMeClone.Models.ViewModels
    {
    public class CustomerViewModel
        {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public List<string> ApartmentNames { get; set; } // Display names of associated apartments
        public List<string> InvoiceIds { get; set; } // Display associated invoice IDs
        }
    }
