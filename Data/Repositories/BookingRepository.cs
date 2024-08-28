using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking GetById(Guid id);
        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);
        OdaDbContext GetContext(); // To be used for finalizing the booking
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly OdaDbContext _context;

        public BookingRepository(OdaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Bookings
                .Include(b => b.Apartment)
                .Include(b => b.Customer)
                .Include(b => b.Invoices)
                .ToList();
        }

        public Booking GetById(Guid id)
        {
            return _context.Bookings
                .Include(b => b.Apartment)
                .Include(b => b.Customer)
                .Include(b => b.Invoices)
                .FirstOrDefault(b => b.BookingId == id);
        }

        public void Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void Update(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }

        public void Delete(Booking booking)
        {
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
        }

        public OdaDbContext GetContext()
        {
            return _context;
        }
    }
}
