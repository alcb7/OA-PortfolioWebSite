using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Services
{
    public interface IAboutMeService
    {
        Task<AboutMe> GetAboutMeByIdAsync(int id);
        Task<IEnumerable<AboutMe>> GetAllAboutMeAsync();
        Task AddAboutMeAsync(AboutMe aboutMe);
        Task UpdateAboutMeAsync(AboutMe aboutMe);
        Task DeleteAboutMeAsync(int id);
    }
}
