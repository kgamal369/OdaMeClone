using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public interface IPackageRepository
        {
        IEnumerable<Package> GetAll();
        Package GetById(Guid id);
        void Add(Package package);
        void Update(Package package);
        void Delete(Package package);
        OdaDbContext GetContext(); // To be used for updating package prices
        }

    public class PackageRepository : IPackageRepository
        {
        private readonly OdaDbContext _context;

        public PackageRepository(OdaDbContext context)
            {
            _context = context;
            }

        public IEnumerable<Package> GetAll()
            {
            return _context.Packages.ToList();
            }

        public Package GetById(Guid id)
            {
            return _context.Packages.FirstOrDefault(p => p.PackageId == id);
            }

        public void Add(Package package)
            {
            _context.Packages.Add(package);
            _context.SaveChanges();
            }

        public void Update(Package package)
            {
            _context.Packages.Update(package);
            _context.SaveChanges();
            }

        public void Delete(Package package)
            {
            _context.Packages.Remove(package);
            _context.SaveChanges();
            }

        public OdaDbContext GetContext()
            {
            return _context;
            }
        }
    }
