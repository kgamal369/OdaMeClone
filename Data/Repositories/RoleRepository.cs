using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public interface IRoleRepository
        {
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        void Add(Role role);
        void Update(Role role);
        void Delete(Role role);
        }

    public class RoleRepository : IRoleRepository
        {
        private readonly OdaDbContext _context;

        public RoleRepository(OdaDbContext context)
            {
            _context = context;
            }

        public IEnumerable<Role> GetAll()
            {
            return _context.Roles
                .Include(r => r.Users)
                .ToList();
            }

        public Role GetById(int id)
            {
            return _context.Roles
                .Include(r => r.Users)
                .FirstOrDefault(r => r.RoleId == id);
            }

        public void Add(Role role)
            {
            _context.Roles.Add(role);
            _context.SaveChanges();
            }

        public void Update(Role role)
            {
            _context.Roles.Update(role);
            _context.SaveChanges();
            }

        public void Delete(Role role)
            {
            _context.Roles.Remove(role);
            _context.SaveChanges();
            }
        }
    }
