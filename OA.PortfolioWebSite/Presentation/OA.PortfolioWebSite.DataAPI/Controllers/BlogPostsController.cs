using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostsService _service;

        public BlogPostsController(IBlogPostsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperienceById(int id)
        {
            var result = await _service.GetBlogPostsByIdAsync(id);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var result = await _service.GetAllBlogPostsAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogPosts([FromBody] BlogPostsCreateDto dto)
        {
            var result = await _service.AddBlogPostsAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return CreatedAtAction(nameof(GetExperienceById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPosts(int id, [FromBody] BlogPostsUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body");
            }

            var result = await _service.UpdateBlogPostsAsync(dto);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var result = await _service.DeleteBlogPostsAsync(id);

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
