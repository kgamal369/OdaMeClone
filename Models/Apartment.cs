using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OdaMeClone.Models
{   
    public enum ApartmentStatus
    {
        Occupied,
        OpenForSale,
        OpenForResale,
        UnderConstruction,
        NotYetUnderConstruction,
        Planning
    }

    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Space { get; set; } // Space in square meters

        [Required]
        public ApartmentStatus Status { get; set; }

        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } // Nullable, as not all apartments are sold

        [Required]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public ICollection<Feature> Features { get; set; } = new List<Feature>();

        public ICollection<Task> Tasks { get; set; } = new List<Task>();

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        private double CalculateTotalPrice()
        {
            double featureCost = Features.Sum(f => f.Cost);
            double invoiceTotal = Invoices.Where(i => i.Status == InvoiceStatus.Paid).Sum(i => i.TotalAmount);  // Changed from Amount to TotalAmount
            double locationFactor = Project.LocationFactor; // Assuming Project has a LocationFactor property

            double basePrice = featureCost + invoiceTotal;
            return basePrice * Space * locationFactor;
        }

        public void ApplyPayment(Payment payment)
        {
            var invoice = Invoices.FirstOrDefault(i => i.Id == payment.InvoiceId);
            if (invoice != null && invoice.Status != InvoiceStatus.Paid)
            {
                invoice.ApplyPayment(payment);
            }
        }
    }
}
