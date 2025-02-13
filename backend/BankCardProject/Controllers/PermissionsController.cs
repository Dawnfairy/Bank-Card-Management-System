using BankCardProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BankCardProject.Controllers
{
    [ApiController]
    [Route("api/rolPermissions")]
    public class PermissionsController : ControllerBase
    {
        private readonly IDatabase _redisDb;
        private readonly IRolePermissionService _rolePermissionService;

        public PermissionsController(IConnectionMultiplexer redis, IRolePermissionService rolePermissionService)
        {
            // Redis üzerinden veritabanına ulaşım sağlıyoruz.
            _redisDb = redis.GetDatabase();
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet("byId/{roleId}")]
        public async Task<IActionResult> GetPermissionsByRoleId(int roleId)
        {
            var permissions = await _rolePermissionService.GetPermissionsByRoleIdAsync(roleId);
            if (permissions == null || permissions.Count == 0)
            {
                return NotFound("Belirtilen role ait yetki bulunamadı.");
            }
            return Ok(permissions);
        }
    }
}
