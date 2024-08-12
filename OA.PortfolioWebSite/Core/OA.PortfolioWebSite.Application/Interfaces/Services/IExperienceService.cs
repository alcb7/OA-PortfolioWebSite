using Ardalis.Result;
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
        Task<Result<IEnumerable<Experiences>>> GetAllExperiencesAsync();
        Task<Result<Experiences>> GetExperienceByIdAsync(int id);
        Task<Result<Experiences>> AddExperienceAsync(ExperiencesCreateDto experienceCreateDto);
        Task<Result<Experiences>> UpdateExperienceAsync(ExperiencesUpdateDto experienceUpdateDto);
        Task<Result> DeleteExperienceAsync(int id);
        Task<Result<IEnumerable<Experiences>>> GetExperienceByUserIdAsync(int userId);

    }
}
