using BankCardProject.DTOs;

namespace BankCardProject.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(UserDto dto);
        Task<bool> LogoutAsync(string userName);
    }
}
