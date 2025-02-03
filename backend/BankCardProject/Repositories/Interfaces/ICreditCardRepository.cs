using BankCardProject.Models;

namespace BankCardProject.Repositories.Interfaces
{
    public interface ICreditCardRepository
    {
        Task<List<CreditCard>> GetAllAsync();
        Task<CreditCard?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreditCard card);
        Task<bool> UpdateAsync(CreditCard card);
        Task<bool> DeleteAsync(int id);
    }
}
