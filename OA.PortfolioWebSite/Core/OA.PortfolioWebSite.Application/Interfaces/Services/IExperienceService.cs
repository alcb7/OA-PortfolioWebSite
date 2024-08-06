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
        Task<IEnumerable<ExperienceDto>> GetAllExperiencesAsync();
        Task<ExperienceDto> GetExperienceByIdAsync(int id);
        Task<ExperienceDto> AddExperienceAsync(ExperiencesCreateDto experienceCreateDto);
        Task<ExperienceDto> UpdateExperienceAsync(int id, ExperiencesUpdateDto experienceUpdateDto);
        Task DeleteExperienceAsync(int id);
    }
}
