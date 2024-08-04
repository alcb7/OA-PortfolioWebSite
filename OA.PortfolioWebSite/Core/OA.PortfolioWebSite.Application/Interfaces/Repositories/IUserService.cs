using OA.PortfolioWebSite.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Repositories
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Register(User user, string password);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> UserExists(string username);
    }
}
