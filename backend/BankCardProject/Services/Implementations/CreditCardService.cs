using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Models;
using BankCardProject.Repositories.Interfaces;
using BankCardProject.Services.Interfaces;

namespace BankCardProject.Services.Implementations
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardRepository CreditCardRepository, IMapper mapper)
        {
            _creditCardRepository = CreditCardRepository;
            _mapper = mapper;
        }


        public async Task<List<CreditCardDto>> GetAllCardsAsync()
        {
            List<CreditCard> creditCards = await _creditCardRepository.GetAllAsync();
            List<CreditCardDto> dtoList = _mapper.Map<List<CreditCardDto>>(creditCards);
            return dtoList;
        }

        public async Task<CreditCardDto> GetCardByIdAsync(int id)
        {
            CreditCard creditCard = await _creditCardRepository.GetByIdAsync(id);
            CreditCardDto dto = _mapper.Map<CreditCardDto>(creditCard);
            return dto;
        }


        public async Task<bool> CreateCardAsync(CreditCardDto dto)
        {
            CreditCard creditCard = _mapper.Map<CreditCard>(dto);
            return await _creditCardRepository.CreateAsync(creditCard);
        }

        public async Task<bool> UpdateCardAsync(int id, CreditCardDto dto)
        {
            var existingCreditCard = await _creditCardRepository.GetByIdAsync(id);
            if (existingCreditCard == null)
            {
                return false;
            }

            _mapper.Map(dto, existingCreditCard);
            return await _creditCardRepository.UpdateAsync(existingCreditCard);
        }

        public async Task<bool> DeleteCardAsync(int id)
        {
            return await _creditCardRepository.DeleteAsync(id);
        }
    }
}
