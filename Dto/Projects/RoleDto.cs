using System;
using System.Collections.Generic;

namespace OdaMeClone.Dtos.Projects
    {
    public class RoleDTO
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<int> UserIds { get; set; } // List of associated user IDs
        public List<int> RolePermissionIds { get; set; } // List of associated role permission IDs
        }
    }
