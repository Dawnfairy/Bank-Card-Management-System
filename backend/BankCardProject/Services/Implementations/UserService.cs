using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Exceptions;
using BankCardProject.Models;
using BankCardProject.Properties;
using BankCardProject.Repositories.Interfaces;
using BankCardProject.Services.Interfaces;
using BCrypt.Net;

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

        /// <summary>
        /// Yeni kullanıcı oluşturur.
        /// </summary>
        public async Task<ApiResponse<bool>> CreateUserAsync(UserDto dto)
        {
            bool isUserExists = await _userRepository.ExistsAsync(dto.UserName);
            if (isUserExists)
            {
                throw new DuplicateRecordException(Resources.CRUD1004);
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = _mapper.Map<User>(dto);
            user.Password = hashedPassword;
            user.IsActive = true;

            await _userRepository.CreateAsync(user);
            return ApiResponse<bool>.SuccessResponse(true);
        }

        /// <summary>
        /// Kullanıcıları getirir.
        /// </summary>
        public async Task<ApiResponse<List<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                throw new NotFoundException(Resources.CRUD2004);
            }

            var dtoList = _mapper.Map<List<UserDto>>(users);
            return ApiResponse<List<UserDto>>.SuccessResponse(dtoList);
        }

        /// <summary>
        /// ID'ye göre kullanıcı getirir.
        /// </summary>
        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException(Resources.CRUD2004);
            }

            var dto = _mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.SuccessResponse(dto);
        }

        /// <summary>
        /// Kullanıcıyı siler (soft delete).
        /// </summary>
        public async Task<ApiResponse<bool>> DeleteUserAsync(int id)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);

            if (existingUser == null)
            {
                throw new NotFoundException(Resources.CRUD2004);
            }

            if (!existingUser.IsActive)
            {
            throw new NotImplementedException();
            }

            existingUser.IsActive = false;
            await _userRepository.UpdateAsync(existingUser);

            return ApiResponse<bool>.SuccessResponse(true);
        }
    }
}
