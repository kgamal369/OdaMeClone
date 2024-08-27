using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public class ResourceRepository : GenericRepository<Resource>
        {
        public ResourceRepository(OdaDbContext context) : base(context)
            {
            }

        // Add any Resource-specific methods here
        }
    }
