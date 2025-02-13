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
            if (dto == null)
                throw new BadRequestException(Resources.CRUD1002);

            var result = await _service.LoginAsync(dto);
            return Ok(new { token = result.Token, role = result.Role });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromQuery] string userName)
        {
            var result = await _service.LogoutAsync(userName);
            return result ? Ok("Çıkış başarılı!") : BadRequest("Çıkış işlemi başarısız.");
        }




    }
}
