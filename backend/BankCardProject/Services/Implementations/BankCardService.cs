using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Models;
using BankCardProject.Repositories.Interfaces;
using BankCardProject.Services.Interfaces;

namespace BankCardProject.Services.Implementations
{
    public class BankCardService : IBankCardService
    { 
        private readonly IBankCardRepository _bankCardRepository;
        private readonly IMapper _mapper;

        public BankCardService(IBankCardRepository bankCardRepository, IMapper mapper)
        {
            _bankCardRepository = bankCardRepository;
            _mapper = mapper;
        }


        public async Task<List<BankCardDto>> GetAllCardsAsync()
        {
            List<BankCard> bankCards = await _bankCardRepository.GetAllAsync();
            List<BankCardDto> dtoList = _mapper.Map<List<BankCardDto>>(bankCards);
            return dtoList;
        }

        public async Task<BankCardDto> GetCardByIdAsync(int id)
        {
            BankCard bankCard = await _bankCardRepository.GetByIdAsync(id);
            BankCardDto dto = _mapper.Map<BankCardDto>(bankCard);
            return dto;
        }


        public async Task<bool> CreateCardAsync(BankCardDto dto)
        {
            BankCard bankCard = _mapper.Map<BankCard>(dto);
            return  await _bankCardRepository.CreateAsync(bankCard);
        }

        public async Task<bool> UpdateCardAsync(int id,BankCardDto dto)
        {
            var existingBankCard = await _bankCardRepository.GetByIdAsync(id);
            if (existingBankCard == null)
            {
                return false;
            } 

            _mapper.Map(dto, existingBankCard);
            return await _bankCardRepository.UpdateAsync(existingBankCard);
        }

        public async Task<bool> DeleteCardAsync(int id)
        {
            return await _bankCardRepository.DeleteAsync(id);
        }
    }
}
