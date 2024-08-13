using Ardalis.Result;
using OA.PortfolioWebSite.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Services
{
    public interface IUserExperienceService
    {
        Task<Result<UserExperienceDto>> GetUserExperienceAsync(int userId);
    }
}
