using OA.PortfolioWebSite.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using OA.PortfolioWebSite.Domain.Entities.Auth;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using Ardalis.Result;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class UserService : IUserService
    {
        private readonly AuthAPIDbContext _context;

        public UserService(AuthAPIDbContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            // Kullanıcıyı Role ile birlikte yüklemek için Include kullanıyoruz
            var user = await _context.Users
                .Include(u => u.Role)  // Role navigasyon özelliğini yükler
                .SingleOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            if (await UserExists(user.Username))
                throw new ApplicationException("Username already exists");

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users
                .Include(u => u.Role)  // Role navigasyon özelliğini yükler
                .Where(u => u.Id == id)
                .Select(u => new User
                {
                    Id = u.Id,
                    RoleId = u.RoleId,
                    Username = u.Username,
                    Name = u.Name,
                    SurName = u.SurName,
                    PasswordHash = "",
                    PasswordSalt = "",
                    Role = new Role
                    {
                        Id = u.RoleId,
                        RoleName = u.Role.RoleName,
                        Users = new User[] { }
                    }
                }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(u => u.Role)  // Role navigasyon özelliğini yükler
                .ToListAsync();
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            var hashBytes = Convert.FromBase64String(storedHash);
            var saltBytes = Convert.FromBase64String(storedSalt);
            using (var hmac = new HMACSHA512(saltBytes))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                var isMatch = hashBytes.SequenceEqual(computedHash);
                Console.WriteLine("Password verification result: " + isMatch);
                return isMatch;
            }
        }
        public async Task<Result<User>> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)  // Role navigasyon özelliğini yükler
                .SingleOrDefaultAsync(u => u.Id == id);  // FindAsync yerine SingleOrDefaultAsync kullanıyoruz

            if (user == null)
                return Result<User>.NotFound();

            return Result<User>.Success(user);
        }

    }

}
