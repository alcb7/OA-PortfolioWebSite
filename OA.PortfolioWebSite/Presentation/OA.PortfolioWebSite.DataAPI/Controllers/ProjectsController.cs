using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Persistance.Services;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _service;

        public ProjectsController(IProjectsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectsById(int id)
        {
            var result = await _service.GetProjectsByIdAsync(id);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await _service.GetAllProjectsAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddProjects([FromBody] ProjectsCreateDto dto)
        {
            var result = await _service.AddProjectsAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return CreatedAtAction(nameof(GetProjectsById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjects(int id, [FromBody] ProjectsUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body");
            }

            var result = await _service.UpdateProjectsAsync(dto);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjects(int id)
        {
            var result = await _service.DeleteProjectsAsync(id);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return NoContent();
        }

    }
}
