using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        private double _amount;

        [Required]
        public double Amount
        {
            get => _amount;
            set
            {
                if (value <= 0) throw new ArgumentException("Amount must be positive.");
                _amount = value;
            }
        }
        
        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public required Invoice Invoice { get; set; }
    }
}
