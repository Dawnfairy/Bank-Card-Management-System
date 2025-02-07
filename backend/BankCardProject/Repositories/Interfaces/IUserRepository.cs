
using BankCardProject.Models;

namespace BankCardProject.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);

        Task<User> CreateAsync(User card);
    }
}
