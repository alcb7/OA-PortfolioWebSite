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
    public interface IEducationService 
    {
        Task<Result<IEnumerable<Educations>>> GetAllEducationsAsync();
        Task<Result<Educations>> GetEducationsByIdAsync(int id);
        Task<Result<Educations>> AddEducationsAsync(EducationsCreateDto dto);
        Task<Result<Educations>> UpdateEducationsAsync(EducationsUpdateDto dto);
        Task<Result> DeleteEducationsAsync(int id);
    }
}
