using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Repositories.Implementations;
using BankCardProject.Repositories.Interfaces;
using BankCardProject.Services.Interfaces;
using BCrypt.Net; // Bunu ekle

namespace BankCardProject.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(UserDto dto)
        {

            /*bool isCardExists = await _userRepository.ExistsAsync(dto.CardNumber);
            if (isCardExists)
            {
                throw new DuplicateRecordException(Resources.CRUD1004);
            }
            */

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            User user = _mapper.Map<User>(dto) ?? throw new OperationFailedException(Resources.ERR1005);
            user.Password = hashedPassword;
            await _userRepository.CreateAsync(user);
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            List<User> users = await _userRepository.GetAllUsersAsync();
            if (users == null || users.Count == 0)
            {
                throw new NotFoundException(Resources.CRUD2004);
            }
            List<UserDto> dtoList = _mapper.Map<List<UserDto>>(users);
            return dtoList ?? throw new OperationFailedException(Resources.ERR1005);

        }

        public Task<UserDto> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

 
    }
}
