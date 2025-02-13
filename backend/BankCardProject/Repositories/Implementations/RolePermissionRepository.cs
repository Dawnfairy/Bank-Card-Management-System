using BankCardProject.Data;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankCardProject.Repositories.Implementations
{
    public class RolePermissionRepository : IRolePermissionRepository
    {

        private readonly AppDbContext _context;

        public RolePermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RolePermission>> GetAllAsync()
        {
            if (_context == null)
            {
                throw new DataAccessException(Resources.ERR1008);
            }
            var rolePermissionList = await _context.RolePermissions.ToListAsync();
            return rolePermissionList;
        }

        public async Task<List<RolePermission>> GetPermissionsByRoleIdAsync(int roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync();
        }


    }
}
