using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Models;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Data.Repositories;

namespace OdaMeClone.Services
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public IEnumerable<InvoiceDTO> GetAllInvoices()
        {
            var invoices = _invoiceRepository.GetAll();
            return invoices.Select(i => new InvoiceDTO
            {
                InvoiceId = i.InvoiceId,
                CustomerId = i.CustomerId,
                ApartmentId = i.ApartmentId,
                BookingId = i.BookingId,
                Amount = i.Amount,
                PaymentMethod = i.PaymentMethod,
                CreatedDateTime = i.CreatedDateTime,
                Status = i.Status,
                DueDate = i.DueDate,
                PaymentDate = i.PaymentDate,
                PaymentStatus = i.PaymentStatus
            });
        }

        public InvoiceDTO GetInvoiceById(Guid id)
        {
            var invoice = _invoiceRepository.GetById(id);
            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }

            return new InvoiceDTO
            {
                InvoiceId = invoice.InvoiceId,
                CustomerId = invoice.CustomerId,
                ApartmentId = invoice.ApartmentId,
                BookingId = invoice.BookingId,
                Amount = invoice.Amount,
                PaymentMethod = invoice.PaymentMethod,
                CreatedDateTime = invoice.CreatedDateTime,
                Status = invoice.Status,
                DueDate = invoice.DueDate,
                PaymentDate = invoice.PaymentDate,
                PaymentStatus = invoice.PaymentStatus
            };
        }

        public void AddInvoice(InvoiceDTO invoiceDTO)
        {
            var invoice = new Invoice
            {
                InvoiceId = Guid.NewGuid(),
                CustomerId = invoiceDTO.CustomerId,
                ApartmentId = invoiceDTO.ApartmentId,
                BookingId = invoiceDTO.BookingId,
                Amount = invoiceDTO.Amount,
                PaymentMethod = invoiceDTO.PaymentMethod,
                CreatedDateTime = DateTime.Now,
                Status = InvoiceStatus.Pending,
                DueDate = invoiceDTO.DueDate,
                PaymentDate = invoiceDTO.PaymentDate,
                PaymentStatus = invoiceDTO.PaymentStatus
            };

            _invoiceRepository.Add(invoice);
        }

        public void UpdateInvoice(Guid id, InvoiceDTO invoiceDTO)
        {
            var invoice = _invoiceRepository.GetById(id);
            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }

            invoice.Amount = invoiceDTO.Amount;
            invoice.PaymentMethod = invoiceDTO.PaymentMethod;
            invoice.Status = invoiceDTO.Status;
            invoice.DueDate = invoiceDTO.DueDate;
            invoice.PaymentDate = invoiceDTO.PaymentDate;
            invoice.PaymentStatus = invoiceDTO.PaymentStatus;

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
