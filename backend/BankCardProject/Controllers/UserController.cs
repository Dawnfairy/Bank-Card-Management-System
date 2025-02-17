using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankCardProject.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Tüm kullanıcıları getirir.
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllUsersAsync();
            return Ok(response);
        }

        /// <summary>
        /// Belirli bir kullanıcıyı ID'ye göre getirir.
        /// </summary>
        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidParameterException(Resources.ERR1015);
            }

            var response = await _service.GetUserByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Yeni bir kullanıcı oluşturur.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            if (dto == null)
                throw new BadRequestException(Resources.CRUD1002);

            var response = await _service.CreateUserAsync(dto);
            return Ok(response);
        }

        /// <summary>
        /// Kullanıcıyı siler (soft delete).
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                throw new InvalidParameterException(Resources.ERR1015);
            }

            var response = await _service.DeleteUserAsync(id);
            return Ok(response);
        }
    }
}
