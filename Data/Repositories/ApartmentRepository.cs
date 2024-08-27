using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public class ApartmentRepository : GenericRepository<Apartment>
        {
        public ApartmentRepository(OdaDbContext context) : base(context)
            {
            }

        // Add any Apartment-specific methods here
        }
    }
