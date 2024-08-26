using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdaMeClone.Models
{
    public enum TaskStatus
    {
        Active,
        InProgress,
        Pending
    }

    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double ManHoursRequired { get; set; }

        [Required]
        public int ResourceId { get; set; }
        [ForeignKey("ResourceId")]
        public Resource Resource { get; set; }

        [Required]
        public int FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        [Required]
        public int ApartmentId { get; set; }
        [ForeignKey("ApartmentId")]
        public Apartment Apartment { get; set; }

        public int? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }

        [Required]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [Required]
        public string Assignee { get; set; } // Assuming Assignee is a string representing the person assigned to the task

        [Required]
        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        public double CalculateTaskCost()
        {
            return ManHoursRequired * (Resource.CostPerHour ?? 0);
        }
    }
}
