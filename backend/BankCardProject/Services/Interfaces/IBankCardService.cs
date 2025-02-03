using BankCardProject.DTOs;
namespace BankCardProject.Services.Interfaces
{
    public interface IBankCardService
    {
        Task<List<BankCardDto>> GetAllCardsAsync();
        Task<BankCardDto> GetCardByIdAsync(int id);
        Task<bool> CreateCardAsync(BankCardDto dto);
        Task<bool> UpdateCardAsync(int id, BankCardDto dto);
        Task<bool> DeleteCardAsync(int id);

    }
}
