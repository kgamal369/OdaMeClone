using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int ApartmentId { get; set; }
        [ForeignKey("ApartmentId")]
        public required Apartment Apartment { get; set; }
    }
}
