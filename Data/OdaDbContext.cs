using Microsoft.EntityFrameworkCore;
using OdaMeClone.Models;

namespace OdaMeClone.Data
    {
    public class OdaDbContext : DbContext
        {
        public OdaDbContext(DbContextOptions<OdaDbContext> options)
            : base(options)
            {
            }

        // DbSet properties representing each entity in the domain model
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<AddOn> AddOns { get; set; }
        public DbSet<ApartmentAddOn> ApartmentAddOns { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);
            // Set the default schema
            modelBuilder.HasDefaultSchema("OdaMeClone");

            // Fluent API configuration for relationships and constraints

            // Customer - Apartment relationship (One-to-Many)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.LinkedApartments)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Customer - Invoice relationship (One-to-Many)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.LinkedInvoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Project - Apartment relationship (One-to-Many)
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Apartments)
                .WithOne(a => a.Project)
                .HasForeignKey(a => a.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Booking - Apartment relationship (One-to-One)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Apartment)
                .WithMany() // Assuming Apartment does not have a direct reference to Booking
                .HasForeignKey(b => b.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Invoice - Payment relationship (One-to-Many)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Invoice)
                .WithMany() // Assuming Invoice does not have a direct reference to Payments
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Package - Apartment relationship (One-to-Many)
            modelBuilder.Entity<Package>()
                .HasMany<Apartment>() // Assuming Package does not have a direct reference to Apartments
                .WithOne(a => a.AssignedPackage)
                .HasForeignKey(a => a.AssignedPackageId)
                .OnDelete(DeleteBehavior.SetNull);

            // Define the many-to-many relationship between Apartment and AddOn
            modelBuilder.Entity<ApartmentAddOn>()
                .HasKey(aa => new { aa.ApartmentId, aa.AddOnId });

            modelBuilder.Entity<ApartmentAddOn>()
                .HasOne(aa => aa.Apartment)
                .WithMany(a => a.AssignedApartmentAddOns)
                .HasForeignKey(aa => aa.ApartmentId);

            modelBuilder.Entity<ApartmentAddOn>()
                .HasOne(aa => aa.AddOn)
                .WithMany(a => a.ApartmentAddOns)
                .HasForeignKey(aa => aa.AddOnId);

            // Ensure decimal precision for monetary values
            modelBuilder.Entity<Invoice>()
                .Property(i => i.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.AmountPaid)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Package>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AddOn>()
                .Property(a => a.PricePerUnit)
                .HasColumnType("decimal(18,2)");

            // Additional configurations can be added as needed
            }
        }
    }
