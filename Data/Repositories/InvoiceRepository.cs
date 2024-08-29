using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public interface IInvoiceRepository
        {
        IEnumerable<Invoice> GetAll();
        Invoice GetById(Guid id);
        void Add(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(Invoice invoice);
        OdaDbContext GetContext(); // To be used for applying payments
        }

    public class InvoiceRepository : IInvoiceRepository
        {
        private readonly OdaDbContext _context;

        public InvoiceRepository(OdaDbContext context)
            {
            _context = context;
            }

        public IEnumerable<Invoice> GetAll()
            {
            return _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Apartment)
                .Include(i => i.Booking)
                .ToList();
            }

        public Invoice GetById(Guid id)
            {
            return _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Apartment)
                .Include(i => i.Booking)
                .FirstOrDefault(i => i.InvoiceId == id);
            }

        public void Add(Invoice invoice)
            {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            }

        public void Update(Invoice invoice)
            {
            _context.Invoices.Update(invoice);
            _context.SaveChanges();
            }

        public void Delete(Invoice invoice)
            {
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
            }

        public OdaDbContext GetContext()
            {
            return _context;
            }
        }
    }
