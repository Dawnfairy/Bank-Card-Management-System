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

        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CreditCardDto>>> GetAllCardsAsync()
        {
            var creditCards = await _creditCardRepository.GetAllAsync();

            if (creditCards == null || !creditCards.Any())
            {
                throw new NotFoundException(Resources.CRUD2004);
            }

            var dtoList = _mapper.Map<List<CreditCardDto>>(creditCards);
            return ApiResponse<List<CreditCardDto>>.SuccessResponse(dtoList);
        }

        public async Task<ApiResponse<CreditCardDto>> GetCardByIdAsync(int id)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);

            if (creditCard == null)
            {
                throw new NotFoundException(Resources.CRUD2001);
            }

            var dto = _mapper.Map<CreditCardDto>(creditCard);
            return ApiResponse<CreditCardDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<bool>> CreateCardAsync(CreditCardDto dto)
        {
            bool isCardExists = await _creditCardRepository.ExistsAsync(dto.CardNumber);
            if (isCardExists)
            {
                throw new DuplicateRecordException(Resources.CRUD1004);
            }

            var creditCard = _mapper.Map<CreditCard>(dto);
            await _creditCardRepository.CreateAsync(creditCard);

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public async Task<ApiResponse<bool>> UpdateCardAsync(int id, CreditCardDto dto)
        {
            var existingCreditCard = await _creditCardRepository.GetByIdAsync(id);

            if (existingCreditCard == null)
            {
                throw new NotFoundException(Resources.CRUD2001);
            }

            _mapper.Map(dto, existingCreditCard);
            await _creditCardRepository.UpdateAsync(existingCreditCard);

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public async Task<ApiResponse<bool>> DeleteCardAsync(int id)
        {
            var existingCreditCard = await _creditCardRepository.GetByIdAsync(id);

            if (existingCreditCard == null)
            {
                throw new NotFoundException(Resources.CRUD2001);
            }

            if (!existingCreditCard.IsActive)
            {
                throw new OperationFailedException(Resources.CRUD4003);
            }

            existingCreditCard.IsActive = false;
            await _creditCardRepository.UpdateAsync(existingCreditCard);

            return ApiResponse<bool>.SuccessResponse(true);
        }
    }
}
