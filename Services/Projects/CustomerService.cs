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

        public IEnumerable<CustomerDTO> GetAllCustomers()
            {
            var customers = _customerRepository.GetAll();
            return customers.Select(c => new CustomerDTO
                {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Email = c.Email,
                ContactNumber = c.ContactNumber,
                LinkedApartmentIds = c.LinkedApartments.Select(a => a.ApartmentId).ToList(),
                LinkedInvoiceIds = c.LinkedInvoices.Select(i => i.InvoiceId).ToList()
                });
            }

        public CustomerDTO GetCustomerById(Guid id)
            {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                {
                throw new KeyNotFoundException("Customer not found");
                }

            return new CustomerDTO
                {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                ContactNumber = customer.ContactNumber,
                LinkedApartmentIds = customer.LinkedApartments.Select(a => a.ApartmentId).ToList(),
                LinkedInvoiceIds = customer.LinkedInvoices.Select(i => i.InvoiceId).ToList()
                };
            }

        public void AddCustomer(CustomerDTO customerDTO)
            {
            var customer = new Customer
                {
                CustomerId = Guid.NewGuid(),
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                ContactNumber = customerDTO.ContactNumber
                };

            _customerRepository.Add(customer);
            }

        public void UpdateCustomer(Guid id, CustomerDTO customerDTO)
            {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                {
                throw new KeyNotFoundException("Customer not found");
                }

            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.ContactNumber = customerDTO.ContactNumber;

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
