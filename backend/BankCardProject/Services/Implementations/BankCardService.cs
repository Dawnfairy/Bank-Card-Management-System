using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
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
            if (bankCards == null || bankCards.Count == 0)
            {
                throw new NotFoundException(Resources.CRUD2004);
            }
            List<BankCardDto> dtoList = _mapper.Map<List<BankCardDto>>(bankCards);
            return dtoList ?? throw new OperationFailedException(Resources.ERR1005);
        }

        public async Task<BankCardDto> GetCardByIdAsync(int id)
        {
            BankCard bankCard = await _bankCardRepository.GetByIdAsync(id) ?? throw new NotFoundException(Resources.CRUD2001);
            BankCardDto dto = _mapper.Map<BankCardDto>(bankCard) ?? throw new OperationFailedException(Resources.ERR1005);
            return dto;
        }


        public async Task CreateCardAsync(BankCardDto dto)
        {
            bool isCardExists = await _bankCardRepository.ExistsAsync(dto.CardNumber);
            if (isCardExists)
            {
                throw new DuplicateRecordException(Resources.CRUD1004);
            }
            BankCard bankCard = _mapper.Map<BankCard>(dto) ?? throw new OperationFailedException(Resources.ERR1005);
            await _bankCardRepository.CreateAsync(bankCard);
        }

        public async Task UpdateCardAsync(int id,BankCardDto dto)
        {
            var existingBankCard = await _bankCardRepository.GetByIdAsync(id) ?? throw new NotFoundException(Resources.CRUD2001);
            _mapper.Map(dto, existingBankCard);
            await _bankCardRepository.UpdateAsync(existingBankCard);
        }

        public async Task DeleteCardAsync(int id)
        {
            var existingBankCard = await _bankCardRepository.GetByIdAsync(id) ?? throw new NotFoundException(Resources.CRUD2001);
            if (!existingBankCard.IsActive)
            {
                throw new OperationFailedException(Resources.CRUD4003);
            }
            existingBankCard.IsActive = false;
            await _bankCardRepository.UpdateAsync(existingBankCard);
        }
    }
}
