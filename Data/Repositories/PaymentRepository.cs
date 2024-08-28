using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment GetById(Guid paymentId);
        void Add(Payment payment);
        void Update(Payment payment);
        void Delete(Payment payment);
        OdaDbContext GetContext();
    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly OdaDbContext _context;

        public PaymentRepository(OdaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Payment> GetAll()
        {
            return _context.Payments
                           .Include(p => p.Customer)  // Include Customer details
                           .Include(p => p.Invoice)   // Include Invoice details
                           .ToList();
        }

        public Payment GetById(Guid paymentId)
        {
            return _context.Payments
                           .Include(p => p.Customer)  // Include Customer details
                           .Include(p => p.Invoice)   // Include Invoice details
                           .FirstOrDefault(p => p.PaymentId == paymentId);
        }
        public void Add(Payment payment )
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

           public void Update(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        public void Delete(Payment payment)
        {
            _context.Payments.Remove(payment);
            _context.SaveChanges();
        }

        public OdaDbContext GetContext()
        {
            return _context;
        }
    }
}
