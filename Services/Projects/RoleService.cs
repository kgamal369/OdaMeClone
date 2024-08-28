using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Models;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Data.Repositories;

namespace OdaMeClone.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<RoleDTO> GetAllRoles()
        {
            var roles = _roleRepository.GetAll();
            return roles.Select(r => new RoleDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt,
                UserIds = r.Users.Select(u => u.Id).ToList(),
                RolePermissionIds = r.RolePermissions.Select(rp => rp.Id).ToList()
            });
        }

        public RoleDTO GetRoleById(int id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                throw new KeyNotFoundException("Role not found");
            }

            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt,
                UserIds = role.Users.Select(u => u.Id).ToList(),
                RolePermissionIds = role.RolePermissions.Select(rp => rp.Id).ToList()
            };
        }

        public void AddRole(RoleDTO roleDTO)
        {
            var role = new Role
            {
                Name = roleDTO.Name,
                Description = roleDTO.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _roleRepository.Add(role);
        }

        public void UpdateRole(int id, RoleDTO roleDTO)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                throw new KeyNotFoundException("Role not found");
            }

            role.Name = roleDTO.Name;
            role.Description = roleDTO.Description;
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
