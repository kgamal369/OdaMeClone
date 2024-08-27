using Microsoft.EntityFrameworkCore;
using OdaMeClone.Models;

namespace OdaMeClone.Data.Repositories
    {
    public class UserRepository : GenericRepository<User>
        {
        public UserRepository(OdaDbContext context) : base(context)
            {
            }

        // Add specific methods for User repository here
        public async Task<User> GetByUsernameAsync(string username)
            {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
            }
        }
    }
