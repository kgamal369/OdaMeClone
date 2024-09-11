using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class CustomerService
        {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
            {
            _customerRepository = customerRepository;
            }

        public IEnumerable<Customer> GetAllCustomers()
            {
            var customers = _customerRepository.GetAll();
            return customers.Select(c => new Customer
                {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Email = c.Email,
                ContactNumber = c.ContactNumber,
                LinkedApartments = (ICollection<Apartment>)c.LinkedApartments.Select(a => a.ApartmentId).ToList(),
                LinkedInvoices = (ICollection<Invoice>)c.LinkedInvoices.Select(i => i.InvoiceId).ToList()
                });
            }

        public Customer GetCustomerById(Guid id)
            {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                {
                throw new KeyNotFoundException("Customer not found");
                }

            return new Customer
                {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                ContactNumber = customer.ContactNumber,
                LinkedApartments = (ICollection<Apartment>)customer.LinkedApartments.Select(a => a.ApartmentId).ToList(),
                LinkedInvoices = (ICollection<Invoice>)customer.LinkedInvoices.Select(i => i.InvoiceId).ToList()
                };
            }

        public void AddCustomer(Customer Customer)
            {
            var customer = new Customer
                {
                CustomerId = Guid.NewGuid(),
                Name = Customer.Name,
                Email = Customer.Email,
                ContactNumber = Customer.ContactNumber
                };

            _customerRepository.Add(customer);
            }

        public void UpdateCustomer(Guid id, Customer Customer)
            {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                {
                throw new KeyNotFoundException("Customer not found");
                }

            customer.Name = Customer.Name;
            customer.Email = Customer.Email;
            customer.ContactNumber = Customer.ContactNumber;

            _customerRepository.Update(customer);
            }

        public void DeleteCustomer(Guid id)
            {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                {
                throw new KeyNotFoundException("Customer not found");
                }

            _customerRepository.Delete(customer);
            }
        }
    }
