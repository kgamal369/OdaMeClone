using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Models;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Repositories;

namespace OdaMeClone.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return users.Select(u => new UserDTO
            {
                Id = u.Id,
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

        public UserDTO GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return new UserDTO
            {
                Id = user.Id,
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

        public void AddUser(UserDTO userDTO, string passwordHash)
        {
            var user = new User
            {
                Username = userDTO.Username,
                PasswordHash = passwordHash,
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber,
                RoleId = userDTO.RoleId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TwoFactorEnabled = userDTO.TwoFactorEnabled
            };

            _userRepository.Add(user);
        }

        public void UpdateUser(int id, UserDTO userDTO)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.Username = userDTO.Username;
            user.Email = userDTO.Email;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.RoleId = userDTO.RoleId;
            user.UpdatedAt = DateTime.UtcNow;
            user.TwoFactorEnabled = userDTO.TwoFactorEnabled;

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
