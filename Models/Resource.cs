using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; } // "Labor", "Material", etc.

        private double? _costPerHour;
        private double? _cost;

        public double? CostPerHour
        {
            get => _costPerHour;
            set
            {
                if (value < 0) throw new ArgumentException("Cost per hour cannot be negative.");
                _costPerHour = value;
            }
        }

        public double? Cost
        {
            get => _cost;
            set
            {
                if (value < 0) throw new ArgumentException("Cost cannot be negative.");
                _cost = value;
            }
        }

        [Required]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}
