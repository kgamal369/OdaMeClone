using System;

namespace OdaMeClone.Models.ViewModels
    {
    public class UserViewModel
        {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string RoleName { get; set; } // Display role name
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public string DisplayName => $"{Username} ({RoleName})";
        }
    }
