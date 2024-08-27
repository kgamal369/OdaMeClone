using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public class FeatureRepository : GenericRepository<Feature>
        {
        public FeatureRepository(OdaDbContext context) : base(context)
            {
            }

        // Add any Feature-specific methods here
        }
    }
