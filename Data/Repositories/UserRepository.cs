using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public interface IUserRepository
        {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        }

    public class UserRepository : IUserRepository
        {
        private readonly OdaDbContext _context;

        public UserRepository(OdaDbContext context)
            {
            _context = context;
            }

        public IEnumerable<User> GetAll()
            {
            return _context.Users
                .Include(u => u.Role)
                .Include(u => u.Projects)
                .ToList();
            }

        public User GetById(int id)
            {
            return _context.Users
                .Include(u => u.Role)
                .Include(u => u.Projects)
                .FirstOrDefault(u => u.Id == id);
            }

        public void Add(User user)
            {
            _context.Users.Add(user);
            _context.SaveChanges();
            }

        public void Update(User user)
            {
            _context.Users.Update(user);
            _context.SaveChanges();
            }

        public void Delete(User user)
            {
            _context.Users.Remove(user);
            _context.SaveChanges();
            }
        }
    }
