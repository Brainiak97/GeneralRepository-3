using Microsoft.AspNetCore.Identity;
using Shared.Auth;
using UserService.BLL.Dto;
using UserService.BLL.Interfaces;
using UserService.DAL.Interfaces;
using UserService.Domain.Models;

namespace UserService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для регистрации, аутентификации и управления пользователями.
    /// </summary>
    public class UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher; // Для хеширования паролей
        private readonly IJwtService _jwtService = jwtService; // Для генерации JWT-токенов

        /// <summary>
        /// Регистрирует нового пользователя на основе предоставленных данных.
        /// </summary>
        /// <param name="request">Объект <see cref="RegisterRequestDto"/>, содержащий данные о пользователе.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="AuthResponseDto"/> с данными аутентификации.</returns>
        /// <exception cref="Exception">Выбрасывается, если логин или email уже заняты.</exception>
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

        /// <summary>
        /// Выполняет вход пользователя по имени пользователя (или email) и паролю.
        /// </summary>
        /// <param name="request">Объект <see cref="LoginRequestDto"/>, содержащий учетные данные пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="AuthResponseDto"/> с данными аутентификации.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь не найден или пароль неверен.</exception>
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

            if (_jwtService.ValidateToken(token, null) == null)
                throw new Exception("Токен доступа поврежден");

            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = token,
            };
        }

        /// <summary>
        /// Находит пользователя по его email-адресу.
        /// </summary>
        /// <param name="email">Email-адрес пользователя для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя в виде <see cref="UserDto"/> или null, если пользователь не найден.</returns>
        public async Task<UserDto?> FindByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            return new UserDto()
            {
                Roles = (ICollection<RoleDto>)user.Roles,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                CreatedAt = user.CreatedAt,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                FirstName = user.FirstName,
                Username = user.Username,
                LastName = user.LastName,
                IsEmailConfirmed = user.IsEmailConfirmed,
                Id = user.Id
            };
        }

        /// <summary>
        /// Находит пользователя по его email-адресу.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, чью информацию требуется отправить.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя в виде <see cref="UserDto"/> или null, если пользователь не найден.</returns>
        public async Task<UserDto?> FindById(int userId)
        {
            var user = await _userRepository.FindByIdAsync(userId) ?? throw new Exception("Пользователь с таким логином не существует");

            if (user == null)
            {
                return null;
            }

            return new UserDto()
            {
                Roles = (ICollection<RoleDto>)user.Roles,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                CreatedAt = user.CreatedAt,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                FirstName = user.FirstName,
                Username = user.Username,
                LastName = user.LastName,
                IsEmailConfirmed = user.IsEmailConfirmed,
                Id = user.Id
            };
        }

        /// <summary>
        /// Подтверждает email пользователя.
        /// </summary>
        /// <param name="email">Email-адрес пользователя, который нужно подтвердить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь не найден.</exception>
        public async Task ConfirmEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email) ?? throw new Exception("Пользователь с таким логином не существует");
            user.IsEmailConfirmed = true;
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Сбрасывает пароль пользователя на новый.
        /// </summary>
        /// <param name="email">Email-адрес пользователя, чей пароль нужно изменить.</param>
        /// <param name="newPassword">Новый пароль, который будет установлен.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь не найден.</exception>
        public async Task ResetPassword(string email, string newPassword)
        {
            var user = await _userRepository.GetUserByEmailAsync(email) ?? throw new Exception("Пользователь с таким логином не существует");
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            await _userRepository.UpdateAsync(user);
        }
    }
}