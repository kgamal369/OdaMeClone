using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OdaMeClone.Models;
using OdaMeClone.Services;

namespace OdaMeClone.Data.Seeders
{
    public class OdaDbSeeder
    {
        private readonly OdaDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public OdaDbSeeder(OdaDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task Seed()
        {
            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(
                    new Role { Name = "Admin", Description = "Administrator" },
                    new Role { Name = "User", Description = "Regular User" }
                );

                await _context.SaveChangesAsync();
            }

            if (!_context.Users.Any())
            {
                _context.Users.Add(new User
                {
                    Username = "admin",
                    Email = "admin@odameclone.com",
                    PasswordHash = _passwordHasher.HashPassword("Admin@123"),
                    RoleId = _context.Roles.First(r => r.Name == "Admin").Id,
                    EmailConfirmed = true
                });

                await _context.SaveChangesAsync();
            }

            // Add any additional seeding logic here
        }
    }
}
