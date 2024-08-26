using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
