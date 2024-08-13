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
        Task<Result<IEnumerable<BlogPosts>>> GetAllExperiencesAsync();
        Task<Result<BlogPosts>> GetExperienceByIdAsync(int id);
        Task<Result<BlogPosts>> AddExperienceAsync(ExperiencesCreateDto experienceCreateDto);
        Task<Result<BlogPosts>> UpdateExperienceAsync(ExperiencesUpdateDto experienceUpdateDto);
        Task<Result> DeleteExperienceAsync(int id);
        Task<Result<IEnumerable<BlogPosts>>> GetExperienceByUserIdAsync(int userId);

    }
}
