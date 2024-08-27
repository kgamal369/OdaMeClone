using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public class InvoiceRepository : GenericRepository<Invoice>
        {
        public InvoiceRepository(OdaDbContext context) : base(context)
            {
            }

        // Add any Invoice-specific methods here
        }
    }
