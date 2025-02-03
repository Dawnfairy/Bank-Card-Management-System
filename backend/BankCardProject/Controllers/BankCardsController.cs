using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Models;
using BankCardProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankCardProject.Controllers
{
    [ApiController]
    [Route("api/bankcards")]
    public class BankCardsController : ControllerBase
    {
        private readonly IBankCardService _service;
        private readonly IMapper _mapper;

        public BankCardsController(IBankCardService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm DebitCard'leri getirir.
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<BankCardDto>>> GetAll()
        {
            var dto = await _service.GetAllCardsAsync();
            if (dto == null)
                return NotFound("Banka kartı listesi boş.");
            return Ok(dto);
        }

        /// <summary>
        /// Belirli bir DebitCard'i Id'ye göre getirir.
        /// </summary>
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<BankCardDto>> GetById(int id)
        {
            var dto = await _service.GetCardByIdAsync(id);

            if (dto == null)
                return NotFound("Id ile eşleşen kart bilgisi bulunamadı.");

            return Ok(dto);
        }

        /// <summary>
        /// Yeni bir BankCard oluşturur.
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<BankCardDto>> Create(BankCardDto dto)
        {
            if (dto == null)
                return BadRequest("BankCard bilgileri boş olamaz.");

            bool isCreated = await _service.CreateCardAsync(dto);

            if (!isCreated)
                return BadRequest("BankCard oluşturulamadı.");

            return Ok(dto);
        }

        /// <summary>
        /// Mevcut bir BankCard'i günceller.
        /// </summary>
        [HttpPut("updateById/{id}")]
        public async Task<IActionResult> Update(int id, BankCardDto cardDto)
        {
            if (cardDto == null)
            {
                return BadRequest(new { Message = "Güncelleme verisi sağlanmalıdır." });
            }
            bool isUpdated = await _service.UpdateCardAsync(id, cardDto);

            if (!isUpdated)
                return BadRequest("BankCard güncellenemedi.");

            return Ok("BankCard başarıyla güncellendi.");
        }

        /// <summary>
        /// Belirli bir DebitCard'i siler.
        /// </summary>
        [HttpDelete("deleteById/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _service.DeleteCardAsync(id);
            if (!isDeleted)
                return BadRequest("BankCard silinemedi.");

            return Ok(isDeleted);
        }
    }
}
