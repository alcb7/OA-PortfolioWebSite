using Microsoft.EntityFrameworkCore;
using OA.PortfolioWebSite.Application.Repositories;
using OA.PortfolioWebSite.Domain.Entities;
using OA.PortfolioWebSite.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            if (await UserExists(user.Username))
                throw new ApplicationException("Username already exists");

            user.PasswordHash = CreatePasswordHash(password);
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

        private string CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            using (var hmac = new HMACSHA512())
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash) == storedHash;
            }
        }
    }
}
