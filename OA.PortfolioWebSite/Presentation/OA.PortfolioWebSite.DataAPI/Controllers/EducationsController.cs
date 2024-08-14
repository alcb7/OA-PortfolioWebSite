using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly IEducationService _service;

        public EducationsController(IEducationService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEducationsById(int id)
        {
            var result = await _service.GetEducationsByIdAsync(id);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEducations()
        {
            var result = await _service.GetAllEducationsAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddEducations([FromBody] EducationsCreateDto dto)
        {
            var result = await _service.AddEducationsAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return CreatedAtAction(nameof(GetEducationsById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducations(int id, [FromBody] EducationsUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body");
            }

            var result = await _service.UpdateEducationsAsync(dto);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducations(int id)
        {
            var result = await _service.DeleteEducationsAsync(id);

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
