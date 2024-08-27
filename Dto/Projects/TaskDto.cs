namespace OdaMeClone.Dtos.Projects
    {
    public class TaskDto
        {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public decimal FixedCost { get; set; }
        public int RequiredManpower { get; set; }
        public int RequiredHours { get; set; }
        public string TaskStatus { get; set; }
        public int ApartmentId { get; set; }
        }
    }

