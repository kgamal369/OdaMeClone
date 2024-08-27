using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public class PaymentRepository : GenericRepository<Payment>
        {
        public PaymentRepository(OdaDbContext context) : base(context)
            {
            }

        // Add any Payment-specific methods here
        }
    }
