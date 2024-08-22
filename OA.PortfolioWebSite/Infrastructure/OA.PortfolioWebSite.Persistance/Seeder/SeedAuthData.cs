using OA.PortfolioWebSite.Domain.Entities.Auth;
using OA.PortfolioWebSite.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Seeder
{
    public static class SeedAuthData
    {
        public static void Initializeauth(AuthAPIDbContext authdbContext)
        {
            SeedRoles(authdbContext);

            SeedUsers(authdbContext);
        }
        public static void SeedRoles(AuthAPIDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
        {
            new Role { RoleName = "admin" },
            new Role { RoleName = "commenter" }
        };

                context.Roles.AddRange(roles);
                context.SaveChanges();
                Console.WriteLine("Seed Roles executed successfully.");
            }
            else
            {
                Console.WriteLine("Roles already exist in the database.");
            }
        }

        public static void SeedUsers(AuthAPIDbContext context)
        {
            if (!context.Users.Any())
            {
                var adminRole = context.Roles.FirstOrDefault(r => r.RoleName == "admin");
                var commenterRole = context.Roles.FirstOrDefault(r => r.RoleName == "commenter");

                if (adminRole != null && commenterRole != null)
                {
                    // Şifreyi hashleyip saltlayacak geçici bir metot
                    void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
                    {
                        using (var hmac = new System.Security.Cryptography.HMACSHA512())
                        {
                            var salt = hmac.Key;
                            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                            passwordSalt = Convert.ToBase64String(salt);
                            passwordHash = Convert.ToBase64String(hash);
                        }
                    }

                    // Admin ve Commenter için hashlenmiş şifre ve salt oluşturun
                    string adminPassword = "password123";
                    CreatePasswordHash(adminPassword, out string adminPasswordHash, out string adminPasswordSalt);

                    string commenterPassword = "password123";
                    CreatePasswordHash(commenterPassword, out string commenterPasswordHash, out string commenterPasswordSalt);

                    var users = new List<User>
            {
                new User
                {
                    Name = "Admin",
                    SurName = "User",
                    Username = "admin",
                    PasswordHash = adminPasswordHash,  // Hashlenmiş şifre
                    PasswordSalt = adminPasswordSalt,  // Salt
                    RoleId = adminRole.Id
                },
                new User
                {
                    Name = "Commenter",
                    SurName = "User",
                    Username = "commenter",
                    PasswordHash = commenterPasswordHash,  // Hashlenmiş şifre
                    PasswordSalt = commenterPasswordSalt,  // Salt
                    RoleId = commenterRole.Id
                }
            };

                    context.Users.AddRange(users);
                    context.SaveChanges();
                    Console.WriteLine("Seed Users executed successfully.");
                }
                else
                {
                    Console.WriteLine("Roles were not found.");
                }
            }
            else
            {
                Console.WriteLine("Users already exist in the database.");
            }
        }



    }
}
