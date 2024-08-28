using System;
using System.Collections.Generic;

namespace OdaMeClone.Dtos.Projects
{

    public class CustomerDTO
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public List<Guid> LinkedApartmentIds { get; set; } // To store associated apartment IDs
        public List<Guid> LinkedInvoiceIds { get; set; } // To store associated invoice IDs
    }
}
