using System.Collections.Generic;
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
            // Seed roles if they don't already exist
            if (!_context.Roles.Any())
                {
                var permissions = new List<Permission>
                {

                    new Permission { EntityName = "Project", Action = "Add" },
                    new Permission { EntityName = "Project", Action = "Edit" },
                    new Permission { EntityName = "Project", Action = "Remove" },
                    new Permission { EntityName = "Apartment", Action = "Add" },
                    new Permission { EntityName = "Apartment", Action = "Edit" },
                    new Permission { EntityName = "Apartment", Action = "Remove" },
                    new Permission { EntityName = "Addon", Action = "Add" },
                    new Permission { EntityName = "Addon", Action = "Edit" },
                    new Permission { EntityName = "Addon", Action = "Remove" },
                    new Permission { EntityName = "Package", Action = "Add" },
                    new Permission { EntityName = "Package", Action = "Edit" },
                    new Permission { EntityName = "Package", Action = "Remove" },
                    new Permission { EntityName = "Booking", Action = "Add" },
                    new Permission { EntityName = "Booking", Action = "Edit" },
                    new Permission { EntityName = "Booking", Action = "Remove" },
                    new Permission { EntityName = "Invoice", Action = "Add" },
                    new Permission { EntityName = "Invoice", Action = "Edit" },
                    new Permission { EntityName = "Invoice", Action = "Remove" },
                    new Permission { EntityName = "Payment", Action = "Add" },
                    new Permission { EntityName = "Payment", Action = "Edit" },
                    new Permission { EntityName = "Payment", Action = "Remove" },
                    new Permission { EntityName = "Customer", Action = "Add" },
                    new Permission { EntityName = "Customer", Action = "Edit" },
                    new Permission { EntityName = "Customer", Action = "Remove" }
                };

                // Create and add roles with permissions
                //admin roles
                var adminRole = new Role
                    {
                    Name = "Admin",
                    Permissions = permissions // Admin has all permissions
                    };

                var superAdminRole = new Role
                    {
                    Name = "SuperAdmin",
                    Permissions = permissions // SuperAdmin has all persmissions
                    };


                // content Creator and Operator Roles
                var contentCreatorRole = new Role
                    {
                    Name = "ContentCreator",
                    Permissions = permissions.Where(p => p.EntityName != "Invoice" && p.EntityName != "Payment").ToList() // ContentCreator can manage content but no access to invoices or payments
                    };

                var operatorRole = new Role
                    {
                    Name = "Operator",
                    Permissions = permissions.Where(p => p.EntityName == "Apartment" || p.EntityName == "Booking").ToList() // Operator can manage apartments and bookings
                    };

                var accountingRole = new Role
                    {
                    Name = "Accounting",
                    Permissions = permissions.Where(p => p.EntityName == "Invoice" || p.EntityName == "Payment").ToList() // Accounting can manage invoices and payments
                    };



                //Guest and normal user and visitor roles
                var guestRole = new Role
                    {
                    Name = "Guest",
                    Permissions = new List<Permission>() // Guest has no permissions
                    };

                var visitorRole = new Role
                    {
                    Name = "Visitor",
                    Permissions = new List<Permission> // Visitor has view permissions only
                    {
                        new Permission { EntityName = "Project", Action = "View" },
                        new Permission { EntityName = "Apartment", Action = "View" },
                        new Permission { EntityName = "Addon", Action = "View" },
                        new Permission { EntityName = "Package", Action = "View" },
                        new Permission { EntityName = "Booking", Action = "View" }
                    }
                    };


                var userRole = new Role
                    {
                    Name = "User",
                    Permissions = permissions.Where(p => p.EntityName == "Project" && p.Action == "Add").ToList() // User can only add projects
                    };

                _context.Roles.AddRange(adminRole, operatorRole, accountingRole, guestRole, userRole, visitorRole, superAdminRole, contentCreatorRole);
                await _context.SaveChangesAsync();
                }

            // Seed users if they don't already exist
            if (!_context.Users.Any())
                {
                _context.Users.Add(new User
                    {
                    Username = "admin",
                    Email = "admin@odameclone.com",
                    PasswordHash = _passwordHasher.HashPassword("Admin@123"),
                    RoleId = _context.Roles.First(r => r.Name == "Admin").RoleId,
                    EmailConfirmed = true
                    });

                _context.Users.Add(new User
                    {
                    Username = "superadmin",
                    Email = "superadmin@odameclone.com",
                    PasswordHash = _passwordHasher.HashPassword("SuperAdmin@123"),
                    RoleId = _context.Roles.First(r => r.Name == "SuperAdmin").RoleId,
                    EmailConfirmed = true
                    });

                _context.Users.Add(new User
                    {
                    Username = "contentcreator",
                    Email = "contentcreator@odameclone.com",
                    PasswordHash = _passwordHasher.HashPassword("ContentCreator@123"),
                    RoleId = _context.Roles.First(r => r.Name == "ContentCreator").RoleId,
                    EmailConfirmed = true
                    });

                _context.Users.Add(new User
                    {
                    Username = "operator",
                    Email = "operator@odameclone.com",
                    PasswordHash = _passwordHasher.HashPassword("Operator@123"),
                    RoleId = _context.Roles.First(r => r.Name == "Operator").RoleId,
                    EmailConfirmed = true
                    });

                _context.Users.Add(new User
                    {
                    Username = "accounting",
                    Email = "accounting@odameclone.com",
                    PasswordHash = _passwordHasher.HashPassword("Accounting@123"),
                    RoleId = _context.Roles.First(r => r.Name == "Accounting").RoleId,
                    EmailConfirmed = true
                    });

                _context.Users.Add(new User
                    {
                    Username = "guest",
                    Email = "guest@odameclone.com",
                    PasswordHash = _passwordHasher.HashPassword("Guest@123"),
                    RoleId = _context.Roles.First(r => r.Name == "Guest").RoleId,
                    EmailConfirmed = true
                    });

                _context.Users.Add(new User
                    {
                    Username = "visitor",
                    Email = "visitor@odameclone.com",
                    PasswordHash = _passwordHasher.HashPassword("Visitor@123"),
                    RoleId = _context.Roles.First(r => r.Name == "Visitor").RoleId,
                    EmailConfirmed = true
                    });

                _context.Users.Add(new User
                    {
                    Username = "user",
                    Email = "user@odameclone.com",
                    PasswordHash = _passwordHasher.HashPassword("User@123"),
                    RoleId = _context.Roles.First(r => r.Name == "User").RoleId,
                    EmailConfirmed = true
                    });

                await _context.SaveChangesAsync();
                }
            }
        }
    }
