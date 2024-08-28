using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
{
    public interface IAddOnRepository
    {
        IEnumerable<AddOn> GetAll();
        AddOn GetById(Guid id);
        void Add(AddOn addOn);
        void Update(AddOn addOn);
        void Delete(AddOn addOn);
    }

    public class AddOnRepository : IAddOnRepository
    {
        private readonly OdaDbContext _context;

        public AddOnRepository(OdaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AddOn> GetAll()
        {
            return _context.AddOns.ToList();
        }

        public AddOn GetById(Guid id)
        {
            return _context.AddOns.FirstOrDefault(a => a.AddOnId == id);
        }

        public void Add(AddOn addOn)
        {
            _context.AddOns.Add(addOn);
            _context.SaveChanges();
        }

        public void Update(AddOn addOn)
        {
            _context.AddOns.Update(addOn);
            _context.SaveChanges();
        }

        public void Delete(AddOn addOn)
        {
            _context.AddOns.Remove(addOn);
            _context.SaveChanges();
        }
    }
}
