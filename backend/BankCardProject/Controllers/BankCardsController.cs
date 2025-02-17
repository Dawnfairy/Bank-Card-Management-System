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
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllCardsAsync();
            return Ok(response);
        }

        /// <summary>
        /// Belirli bir DebitCard'i Id'ye göre getirir.
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
        /// Yeni bir BankCard oluşturur.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create(BankCardDto dto)
        {
            if (dto == null)
                throw new BadRequestException(Resources.CRUD1002);

            var response = await _service.CreateCardAsync(dto);

            return Ok(response);
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
            var response = await _service.UpdateCardAsync(id, cardDto);
            return Ok(response);
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
            var response = await _service.DeleteCardAsync(id);
            return Ok(response);
        }
    }
}
