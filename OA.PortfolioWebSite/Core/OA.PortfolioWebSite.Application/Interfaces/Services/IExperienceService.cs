using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Services
{
    public interface IExperienceService 
    {
        Task<IEnumerable<Experiences>> GetAllExperiencesAsync();
        Task<Experiences> GetExperienceByIdAsync(int id);
        Task AddExperienceAsync(ExperiencesCreateDto experienceCreateDto);
        Task UpdateExperienceAsync(ExperiencesUpdateDto experienceUpdateDto);
        Task DeleteExperienceAsync(int id);
    }
}
