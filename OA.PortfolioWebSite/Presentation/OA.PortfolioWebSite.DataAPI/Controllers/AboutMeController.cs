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
        public async Task<IActionResult> AddAboutMe([FromBody] AboutMeCreateDto aboutMeCreateDto)
        {
            await _aboutMeService.AddAboutMeAsync(aboutMeCreateDto);
            return CreatedAtAction(nameof(GetAboutMeById), new { id = aboutMeCreateDto.Introduction }, aboutMeCreateDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAboutMe(int id, [FromBody] AboutMeUpdateDto aboutMeUpdateDto)
        {
            if (id != aboutMeUpdateDto.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body");
            }

            try
            {
                await _aboutMeService.UpdateAboutMeAsync(aboutMeUpdateDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAboutMe(int id)
        {
            await _aboutMeService.DeleteAboutMeAsync(id);
            return NoContent();
        }
    }
}
