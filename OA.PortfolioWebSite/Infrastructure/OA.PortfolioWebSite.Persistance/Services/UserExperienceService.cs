using Ardalis.Result;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class UserExperienceService : IUserExperienceService
    {
        private readonly IUserService _userService;
        private readonly IExperienceService _experienceService;

        public UserExperienceService(IUserService userService, IExperienceService experienceService)
        {
            _userService = userService;
            _experienceService = experienceService;
        }

        public async Task<Result<UserExperienceDto>> GetUserExperienceAsync(int userId)
        {
            var userResult = await _userService.GetUserByIdAsync(userId);
            if (!userResult.IsSuccess)
            {
                var userErrors = string.Join("; ", userResult.Errors);
                return Result<UserExperienceDto>.Error(userErrors);
            }

            var experienceResult = await _experienceService.GetExperienceByUserIdAsync(userId);
            if (!experienceResult.IsSuccess)
            {
                var experienceErrors = string.Join("; ", experienceResult.Errors);
                return Result<UserExperienceDto>.Error(experienceErrors);
            }

            var userExperienceDto = new UserExperienceDto
            {
                User = userResult.Value,
                Experiences = experienceResult.Value
            };

            return Result<UserExperienceDto>.Success(userExperienceDto);
        }

    }
}
