using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Domain.Entities;
using OA.PortfolioWebSite.Domain.Entities.Auth;

namespace OA.PortfolioWebSite.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            var authenticatedUser = await _userService.Authenticate(loginDto.Username, loginDto.Password);
            if (authenticatedUser == null)
                return Unauthorized();

            var token = _jwtTokenService.GenerateToken(authenticatedUser);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _userService.UserExists(registerDto.Username))
                return BadRequest("Username already exists");

            var newUser = new User
            {
                Username = registerDto.Username,
                Name = registerDto.Name,
                SurName = registerDto.SurName,
            };

            var createdUser = await _userService.Register(newUser, registerDto.Password);
            return Ok(createdUser);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }


    }
}

