using BankCardProject.Models;

namespace BankCardProject.Repositories.Interfaces
{
    public interface ICreditCardRepository
    {
        Task<List<CreditCard>> GetAllAsync();
        Task<CreditCard?> GetByIdAsync(int id);
        Task<CreditCard> CreateAsync(CreditCard card);
        Task UpdateAsync(CreditCard card);
        Task<bool> ExistsAsync(string cardNumber);
    }
}
