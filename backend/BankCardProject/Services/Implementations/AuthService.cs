using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Repositories.Interfaces;
using BankCardProject.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankCardProject.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IDatabase _redisDb;


        public AuthService(IUserRepository userRepository, IConfiguration config, IConnectionMultiplexer redis)
        {
            _userRepository = userRepository;
            _config = config;
            _redisDb = redis.GetDatabase();

        }
        public async Task<ApiResponse<LoginResultDto>> LoginAsync(UserDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.Password))
            {
                throw new BadRequestException(Resources.CRUD1002);
            }

            var user = await _userRepository.GetUserByUsernameAsync(dto.UserName)
                    ?? throw new UnauthorizedException("Kullanıcı adı veya şifre hatalı.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new UnauthorizedException("Kullanıcı adı veya şifre hatalı.");
            }

            var token = GenerateJwtToken(user.UserName, user.RoleId);

            await _redisDb.HashSetAsync($"user:{user.UserName}", new HashEntry[]
            {
                new HashEntry("token", token),
                new HashEntry("role", user.RoleId)
            });

            await _redisDb.KeyExpireAsync($"user:{user.UserName}", TimeSpan.FromHours(1));

            return ApiResponse<LoginResultDto>.SuccessResponse(new LoginResultDto { Token = token, Role = user.RoleId });

        }

        public async Task<ApiResponse<bool>> LogoutAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new UnauthorizedException("Kullanıcı adı veya şifre hatalı.");
            }

            string redisKey = $"user:{userName}";

            try
            {
                bool isDeleted = await _redisDb.KeyDeleteAsync(redisKey);

                if (!isDeleted)
                {
                    throw new UnauthorizedException("Kullanıcı adı veya şifre hatalı.");
                }

                return ApiResponse<bool>.SuccessResponse(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Redis bağlantı hatası oluştu.", ex);
            }
        }

        private string GenerateJwtToken(string userName, int role)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim("role", role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
