using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public interface IApartmentAddOnRepository
        {
        IEnumerable<ApartmentAddOn> GetAll();
        ApartmentAddOn GetByIds(Guid apartmentId, Guid addOnId);
        void Add(ApartmentAddOn apartmentAddOn);
        void Delete(ApartmentAddOn apartmentAddOn);
        void Update(ApartmentAddOn apartmentAddOn);
        }

    public class ApartmentAddOnRepository : IApartmentAddOnRepository
        {
        private readonly OdaDbContext _context;

        public ApartmentAddOnRepository(OdaDbContext context)
            {
            _context = context;
            }

        public IEnumerable<ApartmentAddOn> GetAll()
            {
            // Assuming the ApartmentAddOn entity needs to include both Apartment and AddOn
            return _context.ApartmentAddOns
                .Include(aa => aa.Apartment)
                .Include(aa => aa.AddOn)
                .ToList();
            }
        public ApartmentAddOn GetByIds(Guid apartmentId, Guid addOnId)
            {
            return _context.ApartmentAddOns
                .FirstOrDefault(aa => aa.ApartmentId == apartmentId && aa.AddOnId == addOnId);
            }
        public void Add(ApartmentAddOn apartmentAddOn)
            {
            _context.ApartmentAddOns.Add(apartmentAddOn);
            _context.SaveChanges();
            }

        public void Delete(ApartmentAddOn apartmentAddOn)
            {
            _context.ApartmentAddOns.Remove(apartmentAddOn);
            _context.SaveChanges();
            }

        public void Update(ApartmentAddOn apartmentAddOn)
            {
            _context.ApartmentAddOns.Add(apartmentAddOn);
            _context.SaveChanges();
            }
        }
    }
