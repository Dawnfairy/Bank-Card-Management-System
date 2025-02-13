using BankCardProject.DTOs;

namespace BankCardProject.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(UserDto dto);
        Task<bool> LogoutAsync(string userName);
    }
}
