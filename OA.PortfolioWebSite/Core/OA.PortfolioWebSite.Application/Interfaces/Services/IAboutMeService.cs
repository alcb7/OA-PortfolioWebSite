using OA.PortfolioWebSite.Application.DTOs;
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
        Task AddAboutMeAsync(AboutMeCreateDto aboutMeCreateDto);
        Task UpdateAboutMeAsync(AboutMeUpdateDto aboutMeUpdateDto);
        Task DeleteAboutMeAsync(int id);
    }
}
