using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(Guid id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly OdaDbContext _context;

        public CustomerRepository(OdaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers
                .Include(c => c.LinkedApartments)
                .Include(c => c.LinkedInvoices)
                .ToList();
        }

        public Customer GetById(Guid id)
        {
            return _context.Customers
                .Include(c => c.LinkedApartments)
                .Include(c => c.LinkedInvoices)
                .FirstOrDefault(c => c.CustomerId == id);
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
