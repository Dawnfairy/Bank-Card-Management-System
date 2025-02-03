using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankCardProject.Controllers
{
    [ApiController]
    [Route("api/creditcards")]
    public class CreditCardsController : ControllerBase
    {
        private readonly ICreditCardService _service;
        private readonly IMapper _mapper;

        public CreditCardsController(ICreditCardService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm CreditCard'leri getirir.
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<CreditCardDto>>> GetAll()
        {
            var dto = await _service.GetAllCardsAsync();
            if (dto == null)
                return NotFound("Kredi kartı listesi boş.");
            return Ok(dto);

        }

        /// <summary>
        /// Belirli bir CreditCard'i Id'ye göre getirir.
        /// </summary>
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<CreditCardDto>> GetById(int id)
        {
            var dto = await _service.GetCardByIdAsync(id);

            if (dto == null)
                return NotFound("Id ile eşleşen kart bilgisi bulunamadı.");

            return Ok(dto);
        }

        /// <summary>
        /// Yeni bir CreditCard oluşturur.
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<CreditCardDto>> Create(CreditCardDto dto)
        {
            if (dto == null)
                return BadRequest("CreditCard bilgileri boş olamaz.");

            bool isCreated = await _service.CreateCardAsync(dto);

            if (!isCreated)
                return BadRequest("CreditCard oluşturulamadı.");

            return Ok(dto);
        }

        /// <summary>
        /// Mevcut bir CreditCard'i günceller.
        /// </summary>
        [HttpPut("updateById/{id}")]
        public async Task<IActionResult> Update(int id, CreditCardDto cardDto)
        {
            if (cardDto == null)
            {
                return BadRequest(new { Message = "Güncelleme verisi sağlanmalıdır." });
            }

            bool isUpdated = await _service.UpdateCardAsync(id, cardDto);

            if (!isUpdated)
                return BadRequest("CreditCard güncellenemedi.");
            
            return Ok("CreditCard başarıyla güncellendi.");
        }

        /// <summary>
        /// Belirli bir CreditCard'i siler.
        /// </summary>
        [HttpDelete("deleteById/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _service.DeleteCardAsync(id);
            if (!isDeleted)
                return BadRequest("CreditCard silinemedi.");

            return Ok(isDeleted);
        }
    }
}
