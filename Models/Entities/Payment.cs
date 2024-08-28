using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OdaMeClone.Data;

namespace OdaMeClone.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; } // Primary Key

        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; } // Foreign Key to Customer

        public virtual Customer Customer { get; set; } // Navigation Property

        [Required]
        [ForeignKey("Invoice")]
        public Guid InvoiceId { get; set; } // Foreign Key to Invoice

        public virtual Invoice Invoice { get; set; } // Navigation Property

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 100000000, ErrorMessage = "Amount paid must be a positive value.")]
        public decimal AmountPaid { get; set; } // Amount paid

        [Required]
        public DateTime PaymentDate { get; set; } // Date of payment

        [Required]
        public PaymentMethod PaymentMethod { get; set; } // Enum for payment method

        // Method to register a payment and update the invoice
        public void RegisterPayment(OdaDbContext context)
        {
            if (Invoice == null)
            {
                throw new InvalidOperationException("Payment must be associated with a valid invoice.");
            }

            // Apply the payment to the invoice
            Invoice.ApplyPayment(context, AmountPaid);

            // Save the payment record
            context.Payments.Add(this);
            context.SaveChanges();
        }
    }

}
