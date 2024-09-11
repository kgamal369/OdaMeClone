using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class InvoiceService
        {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
            {
            _invoiceRepository = invoiceRepository;
            }

        public IEnumerable<Invoice> GetAllInvoices()
            {
            var invoices = _invoiceRepository.GetAll();
            return invoices.Select(i => new Invoice
                {
                InvoiceId = i.InvoiceId,
                CustomerId = i.CustomerId,
                ApartmentId = i.ApartmentId,
                BookingId = i.BookingId,
                Amount = i.Amount,
                PaymentMethod = i.PaymentMethod,
                CreatedDateTime = i.CreatedDateTime,
                InvoiceStatus = i.InvoiceStatus,
                DueDate = i.DueDate,
                PaymentDate = i.PaymentDate,
                PaymentStatus = i.PaymentStatus
                });
            }

        public Invoice GetInvoiceById(Guid id)
            {
            var invoice = _invoiceRepository.GetById(id);
            if (invoice == null)
                {
                throw new KeyNotFoundException("Invoice not found");
                }

            return new Invoice
                {
                InvoiceId = invoice.InvoiceId,
                CustomerId = invoice.CustomerId,
                ApartmentId = invoice.ApartmentId,
                BookingId = invoice.BookingId,
                Amount = invoice.Amount,
                PaymentMethod = invoice.PaymentMethod,
                CreatedDateTime = invoice.CreatedDateTime,
                InvoiceStatus = invoice.InvoiceStatus,
                DueDate = invoice.DueDate,
                PaymentDate = invoice.PaymentDate,
                PaymentStatus = invoice.PaymentStatus
                };
            }

        public void AddInvoice(Invoice Invoice)
            {
            var invoice = new Invoice
                {
                InvoiceId = Guid.NewGuid(),
                CustomerId = Invoice.CustomerId,
                ApartmentId = Invoice.ApartmentId,
                BookingId = Invoice.BookingId,
                Amount = Invoice.Amount,
                PaymentMethod = Invoice.PaymentMethod,
                CreatedDateTime = DateTime.Now,
                InvoiceStatus = InvoiceStatus.Pending,
                DueDate = Invoice.DueDate,
                PaymentDate = Invoice.PaymentDate,
                PaymentStatus = Invoice.PaymentStatus
                };

            _invoiceRepository.Add(invoice);
            }

        public void UpdateInvoice(Guid id, Invoice Invoice)
            {
            var invoice = _invoiceRepository.GetById(id);
            if (invoice == null)
                {
                throw new KeyNotFoundException("Invoice not found");
                }

            invoice.Amount = Invoice.Amount;
            invoice.PaymentMethod = Invoice.PaymentMethod;
            invoice.InvoiceStatus = Invoice.InvoiceStatus;
            invoice.DueDate = Invoice.DueDate;
            invoice.PaymentDate = Invoice.PaymentDate;
            invoice.PaymentStatus = Invoice.PaymentStatus;

            _invoiceRepository.Update(invoice);
            }

        public void DeleteInvoice(Guid id)
            {
            var invoice = _invoiceRepository.GetById(id);
            if (invoice == null)
                {
                throw new KeyNotFoundException("Invoice not found");
                }

            _invoiceRepository.Delete(invoice);
            }

        public void ApplyPayment(Guid id, decimal amountPaid)
            {
            var invoice = _invoiceRepository.GetById(id);
            if (invoice == null)
                {
                throw new KeyNotFoundException("Invoice not found");
                }

            invoice.ApplyPayment(_invoiceRepository.GetContext(), amountPaid);
            }
        }
    }
