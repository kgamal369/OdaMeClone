using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public interface IApartmentRepository
        {
        IEnumerable<Apartment> GetAll();
        Apartment GetById(Guid id);
        void Add(Apartment apartment);
        void Update(Apartment apartment);
        void Delete(Apartment apartment);
        }

    public class ApartmentRepository : IApartmentRepository
        {
        private readonly OdaDbContext _context;

        public ApartmentRepository(OdaDbContext context)
            {
            _context = context;
            }

        public IEnumerable<Apartment> GetAll()
            {
            return _context.Apartments.Include(a => a.Project).ToList();
            }

        public Apartment GetById(Guid id)
            {
            return _context.Apartments.Include(a => a.Project).FirstOrDefault(a => a.ApartmentId == id);
            }

        public void Add(Apartment apartment)
            {
            _context.Apartments.Add(apartment);
            _context.SaveChanges();
            }

        public void Update(Apartment apartment)
            {
            _context.Apartments.Update(apartment);
            _context.SaveChanges();
            }

        public void Delete(Apartment apartment)
            {
            _context.Apartments.Remove(apartment);
            _context.SaveChanges();
            }
        }
    }
