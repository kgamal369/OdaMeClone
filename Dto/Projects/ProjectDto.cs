namespace OdaMeClone.Dtos.Projects
    {
    public class ProjectDto
        {
        public int Id { get; set; }
        public string Location { get; set; }
        public List<ApartmentDto> Apartments { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<ResourceDto> Resources { get; set; }
        }
    }
