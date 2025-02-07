using BankCardProject.DTOs;
namespace BankCardProject.Services.Interfaces
{
    public interface ICreditCardService
    {
        Task<List<CreditCardDto>> GetAllCardsAsync();
        Task<CreditCardDto> GetCardByIdAsync(int id);
        Task CreateCardAsync(CreditCardDto dto);
        Task UpdateCardAsync(int id, CreditCardDto dto);
        Task DeleteCardAsync(int id);
    }
}
