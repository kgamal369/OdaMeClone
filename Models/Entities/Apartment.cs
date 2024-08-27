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

    public class LocationFactors
        {
        public string View { get; set; } // E.g., "Sea View", "City View"
        public string Neighborhood { get; set; } // E.g., "Downtown", "Suburbs"
        public decimal ProximityToAmenities { get; set; } // Changed to decimal
        public decimal LocationAdjustmentFactor { get; set; } // Changed to decimal

        public decimal CalculateLocationValueAdjustment()
            {
            // Implement logic to adjust apartment price based on location factors
            return LocationAdjustmentFactor * (ProximityToAmenities > 1 ? 1.1M : 1M); // Example logic
            }
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

        public LocationFactors LocationFactors { get; set; } // Added LocationFactors to Apartment

        public ICollection<Feature> Features { get; set; } = new List<Feature>();

        public ICollection<Task> Tasks { get; set; } = new List<Task>();

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        private decimal CalculateTotalPrice()
            {
            decimal featureCost = Features.Sum(f => (decimal)f.Cost); // Assuming Feature.Cost is of type double
            decimal invoiceTotal = (decimal)Invoices.Where(i => i.Status == InvoiceStatus.Paid).Sum(i => i.TotalAmount);  // Assuming Invoice.TotalAmount is of type decimal
            decimal locationAdjustment = LocationFactors?.CalculateLocationValueAdjustment() ?? 1M; // Adjust based on LocationFactors

            decimal basePrice = featureCost + invoiceTotal;
            return basePrice * (decimal)Space * locationAdjustment; // Convert Space to decimal and apply location adjustment
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
