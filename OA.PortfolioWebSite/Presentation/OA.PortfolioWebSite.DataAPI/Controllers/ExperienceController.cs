using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Persistance.Services;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService _service;

        public ExperienceController(IExperienceService experienceService)
        {
            _service = experienceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperienceById(int id)
        {
            var aboutMe = await _service.GetExperienceByIdAsync(id);
            if (aboutMe == null)
                return NotFound();

            return Ok(aboutMe);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExperiences()
        {
            var aboutMes = await _service.GetAllExperiencesAsync();
            return Ok(aboutMes);
        }

        [HttpPost]
        public async Task<IActionResult> AddExperience([FromBody] ExperiencesCreateDto experienceCreateDto)
        {
            await _service.AddExperienceAsync(experienceCreateDto);
            return CreatedAtAction(nameof(GetExperienceById), new { id = experienceCreateDto.Introduction }, aboutMeCreateDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperience(int id, [FromBody] AboutMeUpdateDto aboutMeUpdateDto)
        {
            if (id != aboutMeUpdateDto.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body");
            }

            try
            {
                await _service.UpdateExperienceAsync(aboutMeUpdateDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            await _service.DeleteExperienceAsync(id);
            return NoContent();
        }
    }
}
