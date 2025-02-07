using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Versioning;

namespace BankCardProject.Controllers
{
    [ApiController]
    [Route("api/bankcards")]
    public class BankCardsController : ControllerBase
    {
        private readonly IBankCardService _service;

        public BankCardsController(IBankCardService service)
        {
            _service = service;
        }

        /// <summary>
        /// Tüm DebitCard'leri getirir.
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<BankCardDto>>> GetAll()
        {
            var dto = await _service.GetAllCardsAsync();
            return Ok(dto);
        }

        /// <summary>
        /// Belirli bir DebitCard'i Id'ye göre getirir.
        /// </summary>
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<BankCardDto>> GetById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidParameterException(Resources.ERR1015);
            }
            var dto = await _service.GetCardByIdAsync(id);

            if (dto == null)
                throw new NotFoundException(Resources.CRUD2001);

            return Ok(dto);
        }

        /// <summary>
        /// Yeni bir BankCard oluşturur.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create(BankCardDto dto)
        {
            if (dto == null)
                throw new BadRequestException(Resources.CRUD1002);

            await _service.CreateCardAsync(dto);

            return Ok(Resources.CRUD1000);
        }

        /// <summary>
        /// Mevcut bir BankCard'i günceller.
        /// </summary>
        [HttpPut("updateById/{id}")]
        public async Task<IActionResult> Update(int id, BankCardDto cardDto)
        {
            if (id <= 0)
            {
                throw new InvalidParameterException(Resources.ERR1015);
            }
            if (cardDto == null)
            {
                throw new BadRequestException(Resources.CRUD3002);
            }
            await _service.UpdateCardAsync(id, cardDto);

            return Ok(Resources.CRUD3000);
        }

        /// <summary>
        /// Belirli bir DebitCard'i siler.
        /// </summary>
        [HttpDelete("deleteById/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                throw new InvalidParameterException(Resources.ERR1015);
            }
            await _service.DeleteCardAsync(id);
     
            return Ok(Resources.CRUD4000);
        }
    }
}
