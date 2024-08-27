namespace OdaMeClone.Data.Repositories
    {
    public class TaskRepository : GenericRepository<Models.Task>
        {
        public TaskRepository(OdaDbContext context) : base(context)
            {
            }

        // Add any Task-specific methods here
        }
    }
