using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
    {
    public class Role
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; } // e.g., "Admin", "User", "Manager"
        [MaxLength(200)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Many-to-many relationship with Permission
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();

        public ICollection<User> Users { get; set; } = new List<User>();
        }

    public class Permission
        {
        [Key]
        public int PermissionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string EntityName { get; set; } // E.g., "Project", "Apartment", etc.

        [Required]
        [MaxLength(50)]
        public string Action { get; set; } // E.g., "Add", "Edit", "Remove"

        // Many-to-many relationship with Role
        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public bool HasPermission(User user, string entityName, string action)
            {
            return user.Role.Permissions.Any(p => p.EntityName == entityName && p.Action == action);
            }
        }

    }
