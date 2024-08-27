using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OdaMeClone.Models
    {
    public enum InvoiceStatus
        {
        Paid,
        Unpaid,
        PartiallyPaid
        }

    public class Invoice
        {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public required Customer Customer { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [Required]
        public int ApartmentId { get; set; }
        [ForeignKey("ApartmentId")]
        public required Apartment Apartment { get; set; }

        [Required]
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;

        public double AmountDue()
            {
            double paidAmount = Payments.Sum(p => p.Amount);
            double dueAmount = TotalAmount - paidAmount;

            if (dueAmount <= 0)
                {
                Status = InvoiceStatus.Paid;
                }
            else if (paidAmount > 0 && paidAmount < TotalAmount)
                {
                Status = InvoiceStatus.PartiallyPaid;
                }
            else
                {
                Status = InvoiceStatus.Unpaid;
                }

            return dueAmount;
            }
        public void ApplyPayment(Payment payment)
            {
            Payments.Add(payment);
            AmountDue(); // Recalculate the due amount and update status
            }
        }
    }
