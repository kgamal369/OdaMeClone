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

        public IEnumerable<Payment> GetAllPayments()
            {
            var payments = _paymentRepository.GetAll();
            return payments.Select(p => new Payment
                {
                PaymentId = p.PaymentId,
                CustomerId = p.CustomerId,
                InvoiceId = p.InvoiceId,
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod
                });
            }

        public Payment GetPaymentById(Guid paymentId)
            {
            var payment = _paymentRepository.GetById(paymentId);
            if (payment == null)
                {
                throw new KeyNotFoundException("Payment not found");
                }

            return new Payment
                {
                PaymentId = payment.PaymentId,
                CustomerId = payment.CustomerId,
                InvoiceId = payment.InvoiceId,
                AmountPaid = payment.AmountPaid,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
                };
            }

        public void AddPayment(Payment Payment)
            {
            var payment = new Payment
                {
                PaymentId = Guid.NewGuid(),
                CustomerId = Payment.CustomerId,
                InvoiceId = Payment.InvoiceId,
                AmountPaid = Payment.AmountPaid,
                PaymentDate = Payment.PaymentDate,
                PaymentMethod = Payment.PaymentMethod
                };

            _paymentRepository.Add(payment);
            }

        public void UpdatePayment(Guid paymentId, Payment Payment)
            {
            var payment = _paymentRepository.GetById(paymentId);
            if (payment == null)
                {
                throw new KeyNotFoundException("Payment not found");
                }

            payment.AmountPaid = Payment.AmountPaid;
            payment.PaymentDate = Payment.PaymentDate;
            payment.PaymentMethod = Payment.PaymentMethod;

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
