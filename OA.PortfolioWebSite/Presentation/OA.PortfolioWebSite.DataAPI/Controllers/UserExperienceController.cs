using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.Interfaces.Services;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserExperienceController : ControllerBase
    {
        private readonly IUserExperienceService _userExperienceService;

        public UserExperienceController(IUserExperienceService userExperienceService)
        {
            _userExperienceService = userExperienceService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserExperience(int userId)
        {
            var result = await _userExperienceService.GetUserExperienceAsync(userId);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
