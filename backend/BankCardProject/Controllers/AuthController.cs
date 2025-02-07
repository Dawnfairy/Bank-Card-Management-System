using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Services.Implementations;
using BankCardProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankCardProject.Controllers
{
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
            if (dto == null)
                throw new BadRequestException(Resources.CRUD1002);

            var token = await _service.LoginAsync(dto);
            return Ok(new { token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string userName)
        {
            var result = await _service.LogoutAsync(userName);
            return result ? Ok("Çıkış başarılı!") : BadRequest("Çıkış işlemi başarısız.");
        }


    }
}
