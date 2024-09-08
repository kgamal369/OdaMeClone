using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OdaMeClone.Data;

namespace OdaMeClone.Models
    {
    public class Booking
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BookingId { get; set; } // Primary Key

        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; } // Foreign Key to Customer

        [Required]
        public Guid ApartmentId { get; set; } // Foreign Key to Apartment

        public virtual Customer Customer { get; set; } // Navigation Property

        public virtual Apartment Apartment { get; set; } // Navigation Property for Apartment

        [Required]
        public DateTime CreatedDateTime { get; set; } // Booking creation date and time

        [Required]
        public BookingStatus BookingStatus { get; set; } // Enum for Booking status

        public DateTime LastModifiedDateTime { get; set; } // Last modified date and time

        [StringLength(100)]
        public string AssignedPerson { get; set; } // Person assigned to this booking

        public virtual ICollection<Invoice> Invoices { get; set; } // List of related invoices

        [NotMapped]
        public decimal RemainingAmount => TotalAmount - (Invoices?.Sum(i => i.Amount) ?? 0); // Calculated field for remaining amount

        [NotMapped]
        public decimal TotalAmount => Apartment?.TotalPrice ?? 0; // Calculated field for total amount

        [Required]
        public PaymentMethod PaymentMethod { get; set; } // Enum for Payment method

        public Booking()
            {
            CreatedDateTime = DateTime.Now;
            Invoices = new List<Invoice>();
            }

        public void StartBooking(OdaDbContext context, Customer customer, Apartment templateApartment)
            {
            if (templateApartment == null)
                throw new ArgumentNullException(nameof(templateApartment), "Template apartment cannot be null.");

            // Clone the template apartment
            var newApartment = new Apartment
                {
                ApartmentId = Guid.NewGuid(),
                ApartmentName = templateApartment.ApartmentName,
                ApartmentType = templateApartment.ApartmentType,
                Space = templateApartment.Space,
                Description = templateApartment.Description,
                ApartmentStatus = ApartmentStatus.Booked,
                ProjectId = templateApartment.ProjectId,
                // Clone other necessary properties
                };

            // Save the cloned apartment to the database
            context.Apartments.Add(newApartment);
            context.SaveChanges();

            // Assign the cloned apartment to this booking
            this.Apartment = newApartment;
            this.CustomerId = customer.CustomerId;

            context.Bookings.Add(this);
            context.SaveChanges();
            }

        public void AssignPackage(OdaDbContext context, Package package)
            {
            if (Apartment == null)
                throw new InvalidOperationException("No apartment assigned to this booking.");

            Apartment.AssignedPackage = package;
            Apartment.CalculateTotalPrice();

            context.SaveChanges();
            }

        public void AssignAddOn(OdaDbContext context, AddOn addon, int quantity)
            {
            if (Apartment == null)
                throw new InvalidOperationException("No apartment assigned to this booking.");

            if (quantity > addon.MaxUnits)
                throw new InvalidOperationException($"Cannot install more than {addon.MaxUnits} units of {addon.AddOnName}.");

            var assignedAddon = Apartment.AssignedAddons.FirstOrDefault(a => a.AddOnId == addon.AddOnId);
            if (assignedAddon == null)
                {
                addon.InstalledUnits = quantity;
                Apartment.AssignedAddons.Add(addon);
                }
            else
                {
                assignedAddon.InstalledUnits += quantity;
                if (assignedAddon.InstalledUnits > addon.MaxUnits)
                    throw new InvalidOperationException($"Total installed units of {addon.AddOnName} exceed the maximum allowed units.");
                }

            Apartment.CalculateTotalPrice();
            context.SaveChanges();
            }

        public void FinalizeBooking(OdaDbContext context)
            {
            if (Apartment == null)
                throw new InvalidOperationException("No apartment assigned to this booking.");

            // Calculate total booking amount
            var totalAmount = Apartment.CalculateTotalPrice();

            // Create and add an invoice
            var invoice = new Invoice
                {
                InvoiceId = Guid.NewGuid(),
                BookingId = this.BookingId,
                CustomerId = this.CustomerId,
                ApartmentId = this.Apartment.ApartmentId,
                Amount = totalAmount,
                PaymentMethod = this.PaymentMethod,
                CreatedDateTime = DateTime.Now,
                InvoiceStatus = InvoiceStatus.Pending
                };

            Invoices.Add(invoice);
            context.Invoices.Add(invoice);
            context.SaveChanges();

            // Update booking status
            this.BookingStatus = BookingStatus.Finalized;
            this.LastModifiedDateTime = DateTime.Now;

            context.SaveChanges();
            }

        public void UpdateBookingStatus(OdaDbContext context, BookingStatus status)
            {
            this.BookingStatus = status;
            this.LastModifiedDateTime = DateTime.Now;

            context.SaveChanges();
            }
        }



    }
