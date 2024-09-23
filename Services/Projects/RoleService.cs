using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class RoleService
        {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
            {
            _roleRepository = roleRepository;
            }

        public IEnumerable<Role> GetAllRoles()
            {
            var roles = _roleRepository.GetAll();
            return roles.Select(r => new Role
                {
                RoleId = r.RoleId,
                Name = r.Name,
                Description = r.Description,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt,
                Users = (ICollection<User>)r.Users.Select(u => u.RoleId).ToList(),
                });
            }

        public Role GetRoleById(int id)
            {
            var role = _roleRepository.GetById(id);
            if (role == null)
                {
                throw new KeyNotFoundException("Role not found");
                }

            return new Role
                {
                RoleId = role.RoleId,
                Name = role.Name,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt,
                Users = (ICollection<User>)role.Users.Select(u => u.RoleId).ToList(),
                };
            }

        public void AddRole(Role Role)
            {
            var role = new Role
                {
                Name = Role.Name,
                Description = Role.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
                };

            _roleRepository.Add(role);
            }

        public void UpdateRole(int id, Role Role)
            {
            var role = _roleRepository.GetById(id);
            if (role == null)
                {
                throw new KeyNotFoundException("Role not found");
                }

            role.Name = Role.Name;
            role.Description = Role.Description;
            role.UpdatedAt = DateTime.UtcNow;

            _roleRepository.Update(role);
            }

        public void DeleteRole(int id)
            {
            var role = _roleRepository.GetById(id);
            if (role == null)
                {
                throw new KeyNotFoundException("Role not found");
                }

            _roleRepository.Delete(role);
            }
        }
    }
