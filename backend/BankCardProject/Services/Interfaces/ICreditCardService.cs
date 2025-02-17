using BankCardProject.DTOs;
using BankCardProject.Models;
namespace BankCardProject.Services.Interfaces
{
    public interface ICreditCardService
    {
        Task<ApiResponse<List<CreditCardDto>>> GetAllCardsAsync();
        Task<ApiResponse<CreditCardDto>> GetCardByIdAsync(int id);
        Task<ApiResponse<bool>> CreateCardAsync(CreditCardDto dto);
        Task<ApiResponse<bool>> UpdateCardAsync(int id, CreditCardDto dto);
        Task<ApiResponse<bool>> DeleteCardAsync(int id);
    }
}
