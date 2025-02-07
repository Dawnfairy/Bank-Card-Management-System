using BankCardProject.DTOs;

namespace BankCardProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task CreateUserAsync(UserDto dto);
        Task DeleteUserAsync(int id);
    }
}
