using BankCardProject.DTOs;
using BankCardProject.Models;

namespace BankCardProject.Services.Interfaces
{
    public interface IRolePermissionService
    {
        Task LoadPermissionIntoRedisAsync();

        Task<ApiResponse<List<RolePermission>>> GetPermissionsByRoleIdAsync(int roleId);

    }
}
