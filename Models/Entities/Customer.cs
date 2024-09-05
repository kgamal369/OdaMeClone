using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
    {
    public class Customer
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerId { get; set; } // Primary Key

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } // Customer's name

        [Required]
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } // Customer's email address

        [Required]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Contact number must be between 10 and 15 digits.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string ContactNumber { get; set; } // Customer's contact number

        public virtual ICollection<Apartment> LinkedApartments { get; set; } // List of owned apartments

        public virtual ICollection<Invoice> LinkedInvoices { get; set; } // List of related invoices

        public Customer()
            {
            LinkedApartments = new List<Apartment>();
            LinkedInvoices = new List<Invoice>();
            }

        // Method to add an apartment to the customer's list
        public void AddApartment(Apartment apartment)
            {
            if (LinkedApartments == null)
                {
                LinkedApartments = new List<Apartment>();
                }
            LinkedApartments.Add(apartment);
            }

        // Method to remove an apartment from the customer's list
        public void RemoveApartment(Apartment apartment)
            {
            LinkedApartments?.Remove(apartment);
            }

        // Method to add an invoice to the customer's list
        public void AddInvoice(Invoice invoice)
            {
            if (LinkedInvoices == null)
                {
                LinkedInvoices = new List<Invoice>();
                }
            LinkedInvoices.Add(invoice);
            }

        // Method to remove an invoice from the customer's list
        public void RemoveInvoice(Invoice invoice)
            {
            LinkedInvoices?.Remove(invoice);
            }

        // Method to get a summary of the customer's details
        public string GetCustomerSummary()
            {
            return $"Customer: {Name}, Email: {Email}, Contact Number: {ContactNumber}";
            }
        }
    }
