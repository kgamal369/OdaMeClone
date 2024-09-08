using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class BookingService
        {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
            {
            _bookingRepository = bookingRepository;
            }

        public IEnumerable<BookingDTO> GetAllBookings()
            {
            var bookings = _bookingRepository.GetAll();
            return bookings.Select(b => new BookingDTO
                {
                BookingId = b.BookingId,
                CustomerId = b.CustomerId,
                ApartmentId = b.ApartmentId,
                CreatedDateTime = b.CreatedDateTime,
                BookingStatus = b.BookingStatus,
                LastModifiedDateTime = b.LastModifiedDateTime,
                AssignedPerson = b.AssignedPerson,
                RemainingAmount = b.RemainingAmount,
                TotalAmount = b.TotalAmount,
                PaymentMethod = b.PaymentMethod,
                Invoices = b.Invoices.Select(i => new InvoiceDTO
                    {
                    InvoiceId = i.InvoiceId,
                    Amount = i.Amount,
                    PaymentMethod = i.PaymentMethod,
                    InvoiceStatus = i.InvoiceStatus
                    }).ToList()
                });
            }

        public BookingDTO GetBookingById(Guid id)
            {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                {
                throw new KeyNotFoundException("Booking not found");
                }

            return new BookingDTO
                {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                ApartmentId = booking.ApartmentId,
                CreatedDateTime = booking.CreatedDateTime,
                BookingStatus = booking.BookingStatus,
                LastModifiedDateTime = booking.LastModifiedDateTime,
                AssignedPerson = booking.AssignedPerson,
                RemainingAmount = booking.RemainingAmount,
                TotalAmount = booking.TotalAmount,
                PaymentMethod = booking.PaymentMethod,
                Invoices = booking.Invoices.Select(i => new InvoiceDTO
                    {
                    InvoiceId = i.InvoiceId,
                    Amount = i.Amount,
                    PaymentMethod = i.PaymentMethod,
                    InvoiceStatus = i.InvoiceStatus
                    }).ToList()
                };
            }

        public void AddBooking(BookingDTO bookingDTO)
            {
            var booking = new Booking
                {
                BookingId = Guid.NewGuid(),
                CustomerId = bookingDTO.CustomerId,
                ApartmentId = bookingDTO.ApartmentId,
                CreatedDateTime = DateTime.Now,
                BookingStatus = BookingStatus.Pending,
                AssignedPerson = bookingDTO.AssignedPerson,
                PaymentMethod = bookingDTO.PaymentMethod
                };

            _bookingRepository.Add(booking);
            }

        public void UpdateBooking(Guid id, BookingDTO bookingDTO)
            {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                {
                throw new KeyNotFoundException("Booking not found");
                }

            booking.BookingStatus = bookingDTO.BookingStatus;
            booking.LastModifiedDateTime = DateTime.Now;
            booking.AssignedPerson = bookingDTO.AssignedPerson;

            _bookingRepository.Update(booking);
            }

        public void DeleteBooking(Guid id)
            {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                {
                throw new KeyNotFoundException("Booking not found");
                }

            _bookingRepository.Delete(booking);
            }

        public void FinalizeBooking(Guid id)
            {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                {
                throw new KeyNotFoundException("Booking not found");
                }

            booking.FinalizeBooking(_bookingRepository.GetContext());
            }
        }
    }
