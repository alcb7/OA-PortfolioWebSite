using Ardalis.Result;
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
        Task<Result<AboutMe>> GetAboutMeByIdAsync(int id);
        Task<Result<IEnumerable<AboutMe>>> GetAllAboutMeAsync();
        Task<Result<AboutMe>> AddAboutMeAsync(AboutMeCreateDto aboutMeCreateDto);
        Task<Result<AboutMe>> UpdateAboutMeAsync(AboutMeUpdateDto aboutMeUpdateDto);
        Task<Result> DeleteAboutMeAsync(int id);
    }
}
