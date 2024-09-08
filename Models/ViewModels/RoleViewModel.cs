using System;
using System.Collections.Generic;

namespace OdaMeClone.Models.ViewModels
    {
    public class RoleViewModel
        {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string>? Usernames { get; set; } // Display names of associated users
        public List<string>? Permissions { get; set; } // Display associated permissions
        }
    }
