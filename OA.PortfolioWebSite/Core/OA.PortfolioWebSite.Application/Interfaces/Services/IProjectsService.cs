using Ardalis.Result;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Domain.Entities;
using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Services
{
    public interface IProjectsService
    {
        Task<Result<IEnumerable<Projects>>> GetAllProjectsAsync();
        Task<Result<Projects>> GetProjectsByIdAsync(int id);
        Task<Result<Projects>> AddProjectsAsync(ProjectsCreateDto dto);
        Task<Result<Projects>> UpdateProjectsAsync(ProjectsUpdateDto dto);
        Task<Result> DeleteProjectsAsync(int id);
    }
}
