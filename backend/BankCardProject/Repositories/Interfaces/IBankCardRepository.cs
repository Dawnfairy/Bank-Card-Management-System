using BankCardProject.Models;

namespace BankCardProject.Repositories.Interfaces
{
    public interface IBankCardRepository
    {
        Task<List<BankCard>> GetAllAsync();
        Task<BankCard?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BankCard card);
        Task<bool> UpdateAsync(BankCard card);
        Task<bool> DeleteAsync(int id);

    }
}
