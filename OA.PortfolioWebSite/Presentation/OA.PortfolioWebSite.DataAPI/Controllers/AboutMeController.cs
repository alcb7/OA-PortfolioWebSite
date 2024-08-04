using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Domain.Entities.Data;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutMeController : ControllerBase
    {
        private readonly IAboutMeService _aboutMeService;

        public AboutMeController(IAboutMeService aboutMeService)
        {
            _aboutMeService = aboutMeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutMeById(int id)
        {
            var aboutMe = await _aboutMeService.GetAboutMeByIdAsync(id);
            if (aboutMe == null)
                return NotFound();

            return Ok(aboutMe);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAboutMe()
        {
            var aboutMes = await _aboutMeService.GetAllAboutMeAsync();
            return Ok(aboutMes);
        }

        [HttpPost]
        public async Task<IActionResult> AddAboutMe([FromBody] AboutMeDto aboutMeDto)
        {
            var aboutMe = new AboutMe
            {
                Introduction = aboutMeDto.Introduction,
                ImageUrl1 = aboutMeDto.ImageUrl1,
                ImageUrl2 = aboutMeDto.ImageUrl2
            };

            await _aboutMeService.AddAboutMeAsync(aboutMe);
            return Ok(aboutMe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAboutMe(int id, [FromBody] AboutMe aboutMe)
        {
            if (id != aboutMe.Id)
                return BadRequest();

            await _aboutMeService.UpdateAboutMeAsync(aboutMe);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAboutMe(int id)
        {
            await _aboutMeService.DeleteAboutMeAsync(id);
            return NoContent();
        }
    }
}
