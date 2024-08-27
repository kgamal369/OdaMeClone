using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public class ProjectRepository : GenericRepository<Project>
        {
        public ProjectRepository(OdaDbContext context) : base(context)
            {
            }

        // Add any Project-specific methods here
        }
    }
