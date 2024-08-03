using OA.PortfolioWebSite.Application.Repositories;
using OA.PortfolioWebSite.Domain.Entities;
using OA.PortfolioWebSite.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

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
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
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
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
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
    }
}
