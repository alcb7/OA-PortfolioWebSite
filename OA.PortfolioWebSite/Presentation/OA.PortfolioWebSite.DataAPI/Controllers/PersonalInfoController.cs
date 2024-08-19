using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalInfoController : ControllerBase
    {
        private readonly IPersonalInfoService _service;

        public PersonalInfoController(IPersonalInfoService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonalInfoById(int id)
        {
            var result = await _service.GetPersonalInfoByIdAsync(id);

            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.ValidationErrors);

                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonalInfo()
        {
            var result = await _service.GetAllPersonalInfoAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ValidationErrors);
            }

            return Ok(result.Value);
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPersonalInfoById(int id)
        //{
        //    var result = await _service.GetPersonalInfoByIdAsync(id);

        //    if (!result.IsSuccess)
        //    {
        //        if (result.Status == ResultStatus.NotFound)
        //            return NotFound(result.ValidationErrors);

        //        return BadRequest(result.ValidationErrors);
        //    }

        //    return Ok(result.Value);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllPersonalInfo()
        //{
        //    var result = await _service.GetAllPersonalInfoAsync();

        //    if (!result.IsSuccess)
        //    {
        //        return BadRequest(result.ValidationErrors);
        //    }

        //    return Ok(result.Value);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddPersonalInfoPersonalInfo([FromBody] PersonalInfoCreateDto dto)
        //{
        //    var result = await _service.AddPersonalInfoAsync(dto);

        //    if (!result.IsSuccess)
        //    {
        //        return BadRequest(result.ValidationErrors);
        //    }

        //    return CreatedAtAction(nameof(GetPersonalInfoById), new { id = result.Value.Id }, result.Value);
        //}

        ////[HttpPut("{id}")]
        ////public async Task<IActionResult> UpdatePersonalInfo(int id, [FromBody] PersonalInfoUpdateDto dto)
        ////{
        ////    if (id != dto.Id)
        ////    {
        ////        return BadRequest("ID in the URL does not match ID in the body");
        ////    }

        ////    var result = await _service.UpdatePersonalInfoAsync(dto);

        ////    if (!result.IsSuccess)
        ////    {
        ////        if (result.Status == ResultStatus.NotFound)
        ////            return NotFound(result.ValidationErrors);

        ////        return BadRequest(result.ValidationErrors);
        ////    }

        ////    return NoContent();
        ////}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePersonalInfo(int id)
        //{
        //    var result = await _service.DeletePersonalInfoAsync(id);

        //    if (!result.IsSuccess)
        //    {
        //        if (result.Status == ResultStatus.NotFound)
        //            return NotFound(result.ValidationErrors);

        //        return BadRequest(result.ValidationErrors);
        //    }

        //    return NoContent();
        //}

    }
}
