using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Models;
using BankCardProject.Repositories.Interfaces;
using BankCardProject.Services.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BankCardProject.Services.Implementations
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IDatabase _redisDb;
        private readonly IMapper _mapper;

        public RolePermissionService(IRolePermissionRepository rolePermissionRepository, IConnectionMultiplexer redis, IMapper mapper)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _redisDb = redis.GetDatabase();
            _mapper = mapper;
        }

        public async Task LoadPermissionIntoRedisAsync()
        {
            var rolePermissions = await _rolePermissionRepository.GetAllAsync();

            var groupedPermissions = rolePermissions.GroupBy(rp => rp.RoleId);

            foreach (var group in groupedPermissions)
            {
                string redisKey = $"permissions:{group.Key}";
                await _redisDb.KeyDeleteAsync(redisKey);
                foreach (var permission in group)
                {
                    string permissionValue = $"{permission.ControllerName}:{permission.ActionName}";
                    await _redisDb.SetAddAsync(redisKey, permissionValue);

                }
                await _redisDb.KeyExpireAsync(redisKey, TimeSpan.FromHours(1));
            }
        }

        public async Task<List<RolePermission>> GetPermissionsByRoleIdAsync(int roleId)
        {
            string redisKey = $"rolePermissions:{roleId}";

            // Redis'ten cache'de izinleri çekiyoruz.
            var permissionsJson = await _redisDb.StringGetAsync(redisKey);
            if (!permissionsJson.IsNullOrEmpty)
            {
                var cachedPermissionsDto = JsonConvert.DeserializeObject<List<RolePermissionDto>>(permissionsJson);
                var cachedPermissions = _mapper.Map<List<RolePermission>>(cachedPermissionsDto);

                return cachedPermissions;
            }
            else
            {
                // Eğer Redis'te bulunamazsa, veritabanından çekiyoruz.
                var permissionsFromDb = await _rolePermissionRepository.GetPermissionsByRoleIdAsync(roleId);
                if (permissionsFromDb == null || !permissionsFromDb.Any())
                {
                    return new List<RolePermission>();
                }

                var permissionsDto = _mapper.Map<List<RolePermissionDto>>(permissionsFromDb);
                var serializedPermissions = JsonConvert.SerializeObject(permissionsDto);
                await _redisDb.StringSetAsync(redisKey, serializedPermissions, TimeSpan.FromHours(1));

                return permissionsFromDb;
            }
        }
    }
}
