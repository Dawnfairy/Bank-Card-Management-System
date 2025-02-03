using BankCardProject.DTOs;
namespace BankCardProject.Services.Interfaces
{
    public interface ICreditCardService
    {
        Task<List<CreditCardDto>> GetAllCardsAsync();
        Task<CreditCardDto> GetCardByIdAsync(int id);
        Task<bool> CreateCardAsync(CreditCardDto dto);
        Task<bool> UpdateCardAsync(int id, CreditCardDto dto);
        Task<bool> DeleteCardAsync(int id);
    }
}
