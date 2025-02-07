using BankCardProject.DTOs;
using BankCardProject.Exceptions;
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

        [HttpGet("all")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var dto = await _service.GetAllUsersAsync();
            return Ok(dto);
        }

        [HttpGet("byId/{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidParameterException(Resources.ERR1015);
            }
            var dto = await _service.GetUserByIdAsync(id);
            if (dto == null)
                throw new NotFoundException(Resources.CRUD2001);
            return Ok(dto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserDto dto)
        {
            if (dto == null)
                throw new BadRequestException(Resources.CRUD1002);
            await _service.CreateUserAsync(dto);
            return Ok(Resources.CRUD1000);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                throw new InvalidParameterException(Resources.ERR1015);
            }
            await _service.DeleteUserAsync(id);
            return Ok(Resources.CRUD1001);
        }

    }
}
