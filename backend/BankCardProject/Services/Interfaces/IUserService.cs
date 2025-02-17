using BankCardProject.DTOs;
using BankCardProject.Models;

namespace BankCardProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<bool>> CreateUserAsync(UserDto dto);
        Task<ApiResponse<List<UserDto>>> GetAllUsersAsync();
        Task<ApiResponse<UserDto>> GetUserByIdAsync(int id);
        Task<ApiResponse<bool>> DeleteUserAsync(int id);
    }
}
