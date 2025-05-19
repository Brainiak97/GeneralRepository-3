using Microsoft.AspNetCore.Identity;
using Shared.Auth;
using UserService.BLL.Dto;
using UserService.BLL.Interfaces;
using UserService.DAL.Interfaces;
using UserService.Domain.Models;

namespace UserService.BLL.Services
{
    public class UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher; // Для хеширования
        private readonly IJwtService _jwtService = jwtService; // Для получения настроек JWT

        public async Task<AuthResponseDto> Register(RegisterRequestDto request)
        {
            // Проверка на существование пользователя
            if (await _userRepository.GetUserByUsernameAsync(request.Username) != null)
                throw new Exception("Такой логин уже используется");

            if (await _userRepository.GetUserByEmailAsync(request.Email) != null)
                throw new Exception("Такой адрес электронной почты уже используется");

            // Создаем пользователя
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.Now,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                IsBlocked = false,
                IsEmailConfirmed = false,
                PasswordHash = string.Empty
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var createdUser = await _userRepository.AddAsync(user);

            // Назначаем роль "User" по умолчанию
            var role = await _userRepository.GetRoleByNameAsync("User");
            if (role != null)
                await _userRepository.AssignRoleToUserAsync(createdUser.Id, role.Id);

            // Генерируем JWT-токен
            string token = _jwtService.GenerateToken(createdUser, createdUser.Roles);

            // Возвращаем ответ с токеном
            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username) ?? throw new Exception("Пользователь с таким логином не существует");
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Неверный пароль");

            string token = _jwtService.GenerateToken(user, user.Roles);

            if (!_jwtService.ValidateToken(token))
                throw new Exception("Токен доступа поврежден");

            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = token,
            };
        }
    }
}
