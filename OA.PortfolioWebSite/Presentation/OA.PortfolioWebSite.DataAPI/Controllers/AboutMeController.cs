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
            var result = await _aboutMeService.GetAboutMeByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAboutMe()
        {
            var result = await _aboutMeService.GetAllAboutMeAsync();
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddAboutMe([FromBody] AboutMeCreateDto aboutMeCreateDto)
        {
            var result = await _aboutMeService.AddAboutMeAsync(aboutMeCreateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetAboutMeById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAboutMe(int id, [FromBody] AboutMeUpdateDto aboutMeUpdateDto)
        {
            if (id != aboutMeUpdateDto.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body");
            }

            var result = await _aboutMeService.UpdateAboutMeAsync(aboutMeUpdateDto);
            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAboutMe(int id)
        {
            var result = await _aboutMeService.DeleteAboutMeAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return NoContent();
        }
    }
}
