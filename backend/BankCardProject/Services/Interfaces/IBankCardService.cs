using BankCardProject.DTOs;
using BankCardProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BankCardProject.Services.Interfaces
{
    public interface IBankCardService
    {
        Task<ApiResponse<List<BankCardDto>>> GetAllCardsAsync();
        Task<ApiResponse<BankCardDto>> GetCardByIdAsync(int id);
        Task<ApiResponse<bool>> CreateCardAsync(BankCardDto dto);
        Task<ApiResponse<bool>> UpdateCardAsync(int id, BankCardDto dto);
        Task<ApiResponse<bool>> DeleteCardAsync(int id);

    }
}
