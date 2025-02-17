using BankCardProject.DTOs;
using BankCardProject.Models;

namespace BankCardProject.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResultDto>> LoginAsync(UserDto dto);
        Task<ApiResponse<bool>> LogoutAsync(string userName);
    }
}
