using BankCardProject.Models;

namespace BankCardProject.Repositories.Interfaces
{
    public interface IBankCardRepository
    {
        Task<List<BankCard>> GetAllAsync();
        Task<BankCard?> GetByIdAsync(int id);
        Task<BankCard> CreateAsync(BankCard card);
        Task UpdateAsync(BankCard card);
        Task<bool> ExistsAsync(string cardNumber);
    }
}
