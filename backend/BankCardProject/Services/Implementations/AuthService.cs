using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Properties;
using BankCardProject.Repositories.Interfaces;
using BankCardProject.Services.Interfaces;
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
        public async Task<string> LoginAsync(UserDto dto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(dto.UserName);


            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new UnauthorizedException(Resources.ERR1011);
            }
            var token = GenerateJwtToken(user.UserName);

            // Token'ı Redis'e kaydet (örneğin, "user_token:{userName}" anahtarıyla, 1 saat geçerlilik)
            await _redisDb.StringSetAsync($"user_token:{user.UserName}", token, TimeSpan.FromHours(1));
            return token;

        }

        public async Task<bool> LogoutAsync(string userName)
        {
            return await _redisDb.KeyDeleteAsync($"user_token:{userName}");
        }

        private string GenerateJwtToken(string userName)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
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
