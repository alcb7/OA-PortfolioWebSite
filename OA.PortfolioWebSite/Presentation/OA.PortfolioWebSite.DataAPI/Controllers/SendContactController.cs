using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Domain.Entities.Data;

namespace OA.PortfolioWebSite.DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendContactController : ControllerBase
    {
        private readonly ISendContactService _contactMessagesService;

        public SendContactController(ISendContactService contactMessagesService)
        {
            _contactMessagesService = contactMessagesService;
        }

        [HttpPost]
        public async Task<IActionResult> PostContactMessage(SendContactDto contactMessage)
        {
            if (contactMessage == null)
            {
                return BadRequest("ContactMessage is null.");
            }

            await _contactMessagesService.SendContactMessageAsync(contactMessage);

            return Ok();
        }
    }
}
