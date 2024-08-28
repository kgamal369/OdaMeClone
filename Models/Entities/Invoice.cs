using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OdaMeClone.Data;

namespace OdaMeClone.Models
{
    public class Invoice
    {
        [Key]
        public Guid InvoiceId { get; set; } // Primary Key

        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; } // Foreign Key to Customer

        public virtual Customer Customer { get; set; } // Navigation Property

        [Required]
        [ForeignKey("Apartment")]
        public Guid ApartmentId { get; set; } // Foreign Key to Apartment

        public virtual Apartment Apartment { get; set; } // Navigation Property

        [Required]
        [ForeignKey("Booking")]
        public Guid BookingId { get; set; } // Foreign Key to Booking

        public virtual Booking Booking { get; set; } // Navigation Property

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 100000000, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; } // Invoice amount
        [Required]
        public PaymentMethod PaymentMethod { get; set; } // Enum for payment method

        [Required]
        public DateTime CreatedDateTime { get; set; } // Date and time the invoice was created

        [Required]
        public InvoiceStatus Status { get; set; } // Enum for invoice status

        [Required]
        public DateTime DueDate { get; set; } // Payment due date

        public DateTime? PaymentDate { get; set; } // Nullable, date of payment

        [Required]
        public PaymentStatus PaymentStatus { get; set; } // Enum for payment status

        // Method to apply a payment and update the invoice status
        public void ApplyPayment(OdaDbContext context, decimal amountPaid)
        {
            if (amountPaid <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero.");
            }

            Amount -= amountPaid;

            if (Amount <= 0)
            {
                Amount = 0; // Ensure amount does not go negative
                PaymentStatus = PaymentStatus.Paid;
                PaymentDate = DateTime.Now;
            }
            else
            {
                PaymentStatus = PaymentStatus.PartiallyPaid;
            }

            context.SaveChanges();
        }
    }


}
