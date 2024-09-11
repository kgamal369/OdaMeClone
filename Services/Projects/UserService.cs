using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class UserService
        {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
            {
            _userRepository = userRepository;
            }

        public IEnumerable<User> GetAllUsers()
            {
            var users = _userRepository.GetAll();
            return users.Select(u => new User
                {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                EmailConfirmed = u.EmailConfirmed,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                LastLogin = u.LastLogin,
                RoleId = u.RoleId,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                TwoFactorEnabled = u.TwoFactorEnabled
                });
            }

        public User GetUserById(int id)
            {
            var user = _userRepository.GetById(id);
            if (user == null)
                {
                throw new KeyNotFoundException("User not found");
                }

            return new User
                {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                LastLogin = user.LastLogin,
                RoleId = user.RoleId,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                TwoFactorEnabled = user.TwoFactorEnabled
                };
            }

        public void AddUser(User User, string passwordHash)
            {
            var user = new User
                {
                Username = User.Username,
                PasswordHash = passwordHash,
                Email = User.Email,
                PhoneNumber = User.PhoneNumber,
                RoleId = User.RoleId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TwoFactorEnabled = User.TwoFactorEnabled
                };

            _userRepository.Add(user);
            }

        public void UpdateUser(int id, User User)
            {
            var user = _userRepository.GetById(id);
            if (user == null)
                {
                throw new KeyNotFoundException("User not found");
                }

            user.Username = User.Username;
            user.Email = User.Email;
            user.PhoneNumber = User.PhoneNumber;
            user.RoleId = User.RoleId;
            user.UpdatedAt = DateTime.UtcNow;
            user.TwoFactorEnabled = User.TwoFactorEnabled;

            _userRepository.Update(user);
            }

        public void DeleteUser(int id)
            {
            var user = _userRepository.GetById(id);
            if (user == null)
                {
                throw new KeyNotFoundException("User not found");
                }

            _userRepository.Delete(user);
            }
        }
    }
