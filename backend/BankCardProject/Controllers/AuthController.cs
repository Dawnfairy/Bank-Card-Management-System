using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Services.Implementations;
using BankCardProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankCardProject.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthService _service;


        public AuthController(IConfiguration config, IAuthService service)
        {
            _config = config;
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            var response = await _service.LoginAsync(dto);
            return Ok(response);
         

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromQuery] string userName)
        {
            var response = await _service.LogoutAsync(userName);
            return Ok(response);
        }
    }
}
