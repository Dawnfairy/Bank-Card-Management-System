using BankCardProject.DTOs;
namespace BankCardProject.Services.Interfaces
{
    public interface IBankCardService
    {
        Task<List<BankCardDto>> GetAllCardsAsync();
        Task<BankCardDto> GetCardByIdAsync(int id);
        Task CreateCardAsync(BankCardDto dto);
        Task UpdateCardAsync(int id, BankCardDto dto);
        Task DeleteCardAsync(int id);

    }
}
