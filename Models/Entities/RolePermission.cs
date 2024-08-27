using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
    {
    public class RolePermission
        {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [Required]
        public string Permission { get; set; } // e.g., "ViewReports", "ManageUsers", "EditProjects"
        }
    }
