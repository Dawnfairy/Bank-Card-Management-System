
using BankCardProject.DTOs;
using BankCardProject.Models;

namespace BankCardProject.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);

        Task<User> CreateAsync(User card);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> ExistsAsync(string userName);
        Task<User?> GetUserByIdAsync(int id);

        Task UpdateAsync(User user);
    }
}
