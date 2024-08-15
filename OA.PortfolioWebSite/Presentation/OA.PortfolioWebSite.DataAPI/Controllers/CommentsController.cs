using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _service;

        public CommentsController(ICommentsService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentsById(int id)
        {
            var result = await _service.GetCommentsByIdAsync(id);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var result = await _service.GetAllCommentsAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddComments([FromBody] CommentsCreateDto dto)
        {
            var result = await _service.AddCommentsAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return CreatedAtAction(nameof(GetCommentsById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonalInfo(int id, [FromBody] CommentsUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body");
            }

            var result = await _service.UpdateCommentsAsync(dto);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComments(int id)
        {
            var result = await _service.DeleteCommentsAsync(id);

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

