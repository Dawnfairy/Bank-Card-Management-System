using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Properties;
using BankCardProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankCardProject.Controllers
{
    [ApiController]
    [Route("api/creditcards")]
    public class CreditCardsController : ControllerBase
    {
        private readonly ICreditCardService _service;

        public CreditCardsController(ICreditCardService service)
        {
            _service = service;
        }

        /// <summary>
        /// Tüm CreditCard'leri getirir.
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<CreditCardDto>>> GetAll()
        {
            var dto = await _service.GetAllCardsAsync();
            return Ok(dto);

        }

        /// <summary>
        /// Belirli bir CreditCard'i Id'ye göre getirir.
        /// </summary>
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<CreditCardDto>> GetById(int id)
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
        /// Yeni bir CreditCard oluşturur.
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<CreditCardDto>> Create(CreditCardDto dto)
        {
            if (dto == null)
                throw new BadRequestException(Resources.CRUD1002);

            await _service.CreateCardAsync(dto);

            return Ok(dto);
        }

        /// <summary>
        /// Mevcut bir CreditCard'i günceller.
        /// </summary>
        [HttpPut("updateById/{id}")]
        public async Task<IActionResult> Update(int id, CreditCardDto cardDto)
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
        /// Belirli bir CreditCard'i siler.
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
