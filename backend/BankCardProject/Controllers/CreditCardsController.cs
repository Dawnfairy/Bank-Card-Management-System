using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
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
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllCardsAsync();
            return Ok(response);
        }

        /// <summary>
        /// Belirli bir CreditCard'i Id'ye göre getirir.
        /// </summary>
        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) 
            {
                throw new InvalidParameterException(Resources.ERR1015);
            }

            var response = await _service.GetCardByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Yeni bir CreditCard oluşturur.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreditCardDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException(Resources.CRUD1002);
            }

            var response = await _service.CreateCardAsync(dto);
            return Ok(response);
        }

        /// <summary>
        /// Mevcut bir CreditCard'i günceller.
        /// </summary>
        [HttpPut("updateById/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreditCardDto cardDto)
        {
            if (id <= 0)
            {
                throw new InvalidParameterException(Resources.ERR1015);
            }
            if (cardDto == null)
            {
                throw new BadRequestException(Resources.CRUD3002);
            }

            var response = await _service.UpdateCardAsync(id, cardDto);
            return Ok(response);
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

            var response = await _service.DeleteCardAsync(id);
            return Ok(response);
        }
    }
}
