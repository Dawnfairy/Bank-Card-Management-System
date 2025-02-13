using BankCardProject.Models;

namespace BankCardProject.Services.Interfaces
{
    public interface IRolePermissionService
    {
        Task LoadPermissionIntoRedisAsync();

        Task<List<RolePermission>> GetPermissionsByRoleIdAsync(int roleId);

    }
}
