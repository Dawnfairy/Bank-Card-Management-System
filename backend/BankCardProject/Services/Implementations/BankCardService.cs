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


        public async Task<ApiResponse<List<BankCardDto>>> GetAllCardsAsync()
        {
            List<BankCard> bankCards = await _bankCardRepository.GetAllAsync();
            if (bankCards == null || !bankCards.Any())
            {
                throw new NotFoundException(Resources.CRUD2004);
            }
            var dtoList = _mapper.Map<List<BankCardDto>>(bankCards);
            return ApiResponse<List<BankCardDto>>.SuccessResponse(dtoList);
        }

        public async Task<ApiResponse<BankCardDto>> GetCardByIdAsync(int id)
        {
            var bankCard = await _bankCardRepository.GetByIdAsync(id);

            if (bankCard == null)
            {
                throw new NotFoundException(Resources.CRUD2001);
            }

            var dto = _mapper.Map<BankCardDto>(bankCard);
            return ApiResponse<BankCardDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<bool>> CreateCardAsync(BankCardDto dto)
        {
            bool isCardExists = await _bankCardRepository.ExistsAsync(dto.CardNumber);
            if (isCardExists)
            {
                throw new BadRequestException(Resources.CRUD1002);
            }

            var bankCard = _mapper.Map<BankCard>(dto);
            await _bankCardRepository.CreateAsync(bankCard);

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public async Task<ApiResponse<bool>> UpdateCardAsync(int id, BankCardDto dto)
        {
            var existingBankCard = await _bankCardRepository.GetByIdAsync(id);

            if (existingBankCard == null)
            {
                throw new NotFoundException(Resources.CRUD2001);
            }

            _mapper.Map(dto, existingBankCard);
            await _bankCardRepository.UpdateAsync(existingBankCard);

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public async Task<ApiResponse<bool>> DeleteCardAsync(int id)
        {
            var existingBankCard = await _bankCardRepository.GetByIdAsync(id);

            if (existingBankCard == null)
            {
                throw new NotFoundException(Resources.CRUD2001);
            }

            if (!existingBankCard.IsActive)
            {
                throw new NotFoundException(Resources.CRUD2001);
            }

            existingBankCard.IsActive = false;
            await _bankCardRepository.UpdateAsync(existingBankCard);

            return ApiResponse<bool>.SuccessResponse(true);
        }
    }
}
