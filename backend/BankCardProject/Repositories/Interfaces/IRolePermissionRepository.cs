using BankCardProject.Models;

namespace BankCardProject.Repositories.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<List<RolePermission>> GetAllAsync();

        Task<List<RolePermission>> GetPermissionsByRoleIdAsync(int roleId);


    }
}
