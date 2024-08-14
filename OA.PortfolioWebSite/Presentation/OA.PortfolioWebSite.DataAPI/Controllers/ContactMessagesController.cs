using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessagesController : ControllerBase
    {
        private readonly IContactMessagesService _service;

        public ContactMessagesController(IContactMessagesService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactMessagesById(int id)
        {
            var result = await _service.GetContactMessagesByIdAsync(id);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContactMessages()
        {
            var result = await _service.GetAllContactMessagesAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddGetContactMessages([FromBody] ContactMessagesCreateDto dto)
        {
            var result = await _service.AddContactMessagesAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return CreatedAtAction(nameof(GetContactMessagesById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGetContactMessageso(int id, [FromBody] ContactMessagesUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body");
            }

            var result = await _service.UpdateContactMessagesAsync(dto);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGetContactMessages(int id)
        {
            var result = await _service.DeleteContactMessagesAsync(id);

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
