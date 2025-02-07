using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
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
            if(creditCards == null || creditCards.Count == 0)
            {
                throw new NotFoundException(Resources.CRUD2004);
            }
            List<CreditCardDto> dtoList = _mapper.Map<List<CreditCardDto>>(creditCards);
            return dtoList ?? throw new OperationFailedException(Resources.ERR1005);
        }

        public async Task<CreditCardDto> GetCardByIdAsync(int id)
        {
            CreditCard creditCard = await _creditCardRepository.GetByIdAsync(id) ?? throw new NotFoundException(Resources.CRUD2001);
            CreditCardDto dto = _mapper.Map<CreditCardDto>(creditCard) ?? throw new OperationFailedException(Resources.ERR1005);
            return dto;
        }


        public async Task CreateCardAsync(CreditCardDto dto)
        {
            bool isCardExists = await _creditCardRepository.ExistsAsync(dto.CardNumber);
            if (isCardExists)
            {
                throw new DuplicateRecordException(Resources.CRUD1004);
            }
            CreditCard creditCard = _mapper.Map<CreditCard>(dto) ?? throw new OperationFailedException(Resources.ERR1005);
            await _creditCardRepository.CreateAsync(creditCard);
        }

        public async Task UpdateCardAsync(int id, CreditCardDto dto)
        {
            var existingCreditCard = await _creditCardRepository.GetByIdAsync(id) ?? throw new NotFoundException(Resources.CRUD2001);
            _mapper.Map(dto, existingCreditCard);
            await _creditCardRepository.UpdateAsync(existingCreditCard);
        }

        public async Task DeleteCardAsync(int id)
        {
            var existingCreditCard = await _creditCardRepository.GetByIdAsync(id) ?? throw new NotFoundException(Resources.CRUD2001);
            if (!existingCreditCard.IsActive)
            {
                throw new OperationFailedException(Resources.CRUD4003);
            }
            existingCreditCard.IsActive = false;
            await _creditCardRepository.UpdateAsync(existingCreditCard);
        }
    }
}
