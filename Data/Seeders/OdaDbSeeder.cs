using System.Linq;
using System.Threading.Tasks;  // Fully qualify Task for asynchronous methods
using Microsoft.Extensions.DependencyInjection;
using OdaMeClone.Models;
using OdaMeClone.Services;

namespace OdaMeClone.Data.Seeders
    {
    public static class OdaDbSeeder
        {
        public static async System.Threading.Tasks.Task SeedAsync(IServiceProvider serviceProvider)  // Fully qualify Task here
            {
            using (var scope = serviceProvider.CreateScope())
                {
                var context = scope.ServiceProvider.GetRequiredService<OdaDbContext>();

                if (!context.Roles.Any())
                    {
                    context.Roles.AddRange(
                        new Role { Name = "Admin", Description = "Administrator" },
                        new Role { Name = "User", Description = "Regular User" }
                    );

                    await context.SaveChangesAsync();
                    }

                if (!context.Users.Any())
                    {
                    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

                    context.Users.Add(new User
                        {
                        Username = "admin",
                        Email = "admin@odameclone.com",
                        PasswordHash = passwordHasher.HashPassword("Admin@123"),
                        RoleId = context.Roles.First(r => r.Name == "Admin").Id,
                        EmailConfirmed = true
                        });

                    await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
