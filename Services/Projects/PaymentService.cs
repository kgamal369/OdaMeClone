using System;
using System.Collections.Generic;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class PaymentService
        {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
            {
            _paymentRepository = paymentRepository;
            }

        public IEnumerable<PaymentDTO> GetAllPayments()
            {
            var payments = _paymentRepository.GetAll();
            return payments.Select(p => new PaymentDTO
                {
                PaymentId = p.PaymentId,
                CustomerId = p.CustomerId,
                InvoiceId = p.InvoiceId,
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod
                });
            }

        public PaymentDTO GetPaymentById(Guid paymentId)
            {
            var payment = _paymentRepository.GetById(paymentId);
            if (payment == null)
                {
                throw new KeyNotFoundException("Payment not found");
                }

            return new PaymentDTO
                {
                PaymentId = payment.PaymentId,
                CustomerId = payment.CustomerId,
                InvoiceId = payment.InvoiceId,
                AmountPaid = payment.AmountPaid,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
                };
            }

        public void AddPayment(PaymentDTO paymentDTO)
            {
            var payment = new Payment
                {
                PaymentId = Guid.NewGuid(),
                CustomerId = paymentDTO.CustomerId,
                InvoiceId = paymentDTO.InvoiceId,
                AmountPaid = paymentDTO.AmountPaid,
                PaymentDate = paymentDTO.PaymentDate,
                PaymentMethod = paymentDTO.PaymentMethod
                };

            _paymentRepository.Add(payment);
            }

        public void UpdatePayment(Guid paymentId, PaymentDTO paymentDTO)
            {
            var payment = _paymentRepository.GetById(paymentId);
            if (payment == null)
                {
                throw new KeyNotFoundException("Payment not found");
                }

            payment.AmountPaid = paymentDTO.AmountPaid;
            payment.PaymentDate = paymentDTO.PaymentDate;
            payment.PaymentMethod = paymentDTO.PaymentMethod;

            _paymentRepository.Update(payment);
            }

        public void DeletePayment(Guid paymentId)
            {
            var payment = _paymentRepository.GetById(paymentId);
            if (payment == null)
                {
                throw new KeyNotFoundException("Payment not found");
                }

            _paymentRepository.Delete(payment);
            }
        }
    }
