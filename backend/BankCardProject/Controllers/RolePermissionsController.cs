using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BankCardProject.Controllers
{
    [ApiController]
    [Route("api/rolePermissions")]
    public class RolePermissionsController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionsController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        /// <summary>
        /// Belirli bir role ait yetkileri getirir.
        /// </summary>
        [HttpGet("byId/{roleId}")]
        public async Task<IActionResult> GetPermissionsByRoleId(int roleId)
        {
            if (roleId < 0)
            {
                throw new BadRequestException(Resources.CRUD1002);
            }
      

            var response = await _rolePermissionService.GetPermissionsByRoleIdAsync(roleId);
            return Ok(response);
        }

        /// <summary>
        /// Tüm yetkileri Redis'e yükler.
        /// </summary>
        [HttpPost("loadIntoRedis")]
        public async Task<IActionResult> LoadPermissionsIntoRedis()
        {
            await _rolePermissionService.LoadPermissionIntoRedisAsync();
            return Ok(ApiResponse<bool>.SuccessResponse(true));
        }
    }
}
