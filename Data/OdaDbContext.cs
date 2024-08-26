using Microsoft.EntityFrameworkCore;
using OdaMeClone.Models;

namespace OdaMeClone.Data
{
    public class OdaDbContext : DbContext
    {
        public OdaDbContext(DbContextOptions<OdaDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<OdaMeClone.Models.Task> Tasks { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Role Relationship
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Role>()
                .HasKey(r => r.Id);

            // Project-Creator Relationship
            modelBuilder.Entity<Project>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Project>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.Creator)
                .OnDelete(DeleteBehavior.Cascade);

            // Task-Project Relationship
            modelBuilder.Entity<OdaMeClone.Models.Task>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<OdaMeClone.Models.Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Task-Assignee Relationship
            modelBuilder.Entity<OdaMeClone.Models.Task>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.Assignee)
                .OnDelete(DeleteBehavior.Restrict);

            // Resource-Project Relationship
            modelBuilder.Entity<Resource>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Project)
                .WithMany(p => p.Resources)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Invoice-Apartment Relationship
            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Apartment)
                .WithMany(a => a.Invoices)
                .HasForeignKey(i => i.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment-Invoice Relationship
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Invoice)
                .WithMany(i => i.Payments)
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Apartment-Project Relationship
            modelBuilder.Entity<Apartment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Project)
                .WithMany(p => p.Apartments)
                .HasForeignKey(a => a.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Apartment-Customer Relationship
            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Apartments)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Feature-Apartment Relationship
            modelBuilder.Entity<Feature>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Feature>()
                .HasOne(f => f.Apartment)
                .WithMany(a => a.Features)
                .HasForeignKey(f => f.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
