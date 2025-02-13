using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Properties;
using BankCardProject.Repositories.Interfaces;
using BankCardProject.Services.Interfaces;
using BankCardProject.DTOs;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
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
        public async Task<LoginResult> LoginAsync(UserDto dto)
        {
            //Authentication
            var user = await _userRepository.GetUserByUsernameAsync(dto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new UnauthorizedException(Resources.ERR1011);
            }

            int role = user.RoleId;
            var token = GenerateJwtToken(user.UserName, role);

            string redisKey = $"user:{user.UserName}";
            await _redisDb.HashSetAsync(redisKey, new HashEntry[]
            {
                new HashEntry("token", token),
                new HashEntry("role", role)
            });

            await _redisDb.KeyExpireAsync(redisKey, TimeSpan.FromHours(1));

            return new LoginResult
            {
                Token = token,
                Role = role
            };

        }

        public async Task<bool> LogoutAsync(string userName)
        {
            string redisKey = $"user:{userName}";
            return await _redisDb.KeyDeleteAsync(redisKey);
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
