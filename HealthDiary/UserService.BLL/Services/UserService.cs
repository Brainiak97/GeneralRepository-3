using AutoMapper;
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
    public class UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher; // Для хеширования паролей
        private readonly IJwtService _jwtService = jwtService; // Для генерации JWT-токенов
        private readonly IMapper _mapper = mapper; // Для генерации JWT-токенов

        /// <summary>
        /// Регистрирует нового пользователя на основе предоставленных данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="request">Объект <see cref="RegisterRequestDto"/>, содержащий данные о пользователе.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="AuthResponseDto"/> с данными аутентификации.</returns>
        /// <exception cref="Exception">Выбрасывается, если логин или email уже заняты.</exception>
        public async Task<AuthResponseDto> Register(RegisterRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(5000, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Запрос был отменён пользователем.");
            }

            // Проверка на существование пользователя
            if (await _userRepository.GetUserByUsernameAsync(request.Username, cancellationToken) != null)
                throw new Exception("Такой логин уже используется");

            if (await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken) != null)
                throw new Exception("Такой адрес электронной почты уже используется");

            // Создаем пользователя
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                Status = UserStatus.Active,
                IsEmailConfirmed = false,
                PasswordHash = string.Empty
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var createdUser = await _userRepository.AddAsync(user, cancellationToken);

            // Назначаем роль "User" по умолчанию
            var role = await _userRepository.GetRoleByNameAsync("User", cancellationToken);
            if (role != null)
                await _userRepository.AssignRoleToUserAsync(createdUser.Id, role.Id, cancellationToken);

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
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="request">Объект <see cref="LoginRequestDto"/>, содержащий учетные данные пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="AuthResponseDto"/> с данными аутентификации.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь не найден или пароль неверен.</exception>
        public async Task<AuthResponseDto> Login(LoginRequestDto request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username, cancellationToken) ?? throw new Exception("Пользователь с таким логином не существует");

            if (user.Status == UserStatus.Blocked)
                throw new Exception("Пользователь заблокирован");

            if (user.Status == UserStatus.Deleted)
                throw new Exception("Пользователь удалён");

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
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="email">Email-адрес пользователя для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя в виде <see cref="UserDto"/> или null, если пользователь не найден.</returns>
        public async Task<UserDto?> FindByEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Находит пользователя по его email-адресу.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="userId">Идентификатор пользователя, чью информацию требуется отправить.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя в виде <see cref="UserDto"/> или null, если пользователь не найден.</returns>
        public async Task<UserDto?> FindById(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken) ?? throw new Exception("Пользователь с таким логином не существует");

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Подтверждает email пользователя.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="email">Email-адрес пользователя, который нужно подтвердить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь не найден.</exception>
        public async Task ConfirmEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(email, cancellationToken) ?? throw new Exception("Пользователь с таким логином не существует");
            user.IsEmailConfirmed = true;
            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// Сбрасывает пароль пользователя на новый.
        /// </summary>
        /// <param name="email">Email-адрес пользователя, чей пароль нужно изменить.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="newPassword">Новый пароль, который будет установлен.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь не найден.</exception>
        public async Task ResetPassword(string email, string newPassword, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(email, cancellationToken) ?? throw new Exception("Пользователь с таким логином не существует");
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// Выдает список всех пользователей в системе.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденных пользователей в виде списка <see cref="UserDto"/> или null, если пользователи не найдены.</returns>
        public async Task<IEnumerable<UserDto?>> GetAll(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers(cancellationToken);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        /// <summary>
        /// Обновляет данные пользователя в хранилище.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="dto"></param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        public async Task<bool> UpdateUserAsync(int userId, UserUpdateDto dto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user == null) return false;

            user.DateOfBirth = DateTime.SpecifyKind(dto.DateOfBirth, DateTimeKind.Utc);

            await _userRepository.UpdateAsync(_mapper.Map(dto, user), cancellationToken);
            return true;
        }

        /// <summary>
        /// Отмечает пользователя как удаленного.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        public async Task<bool> DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user == null) return false;

            // Мягкое удаление
            user.Status = UserStatus.Deleted;
            user.DeletedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user, cancellationToken);
            return true;
        }

        /// <summary>
        /// Восстанавливает пользователя.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>  
        public async Task<bool> RestoreUserAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user == null || user.Status != UserStatus.Deleted) return false;

            user.Status = UserStatus.Active;
            user.DeletedAt = null;

            await _userRepository.UpdateAsync(user, cancellationToken);
            return true;
        }

        /// <summary>
        /// Блокировка и разблокировка пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="isBlocked">Указывает нужно заблокировать или разблокировать пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь помечен как удаленный.</exception>
        public async Task<bool> BlockUserAsync(int userId, bool isBlocked, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken) ?? throw new Exception("Пользователь не найден.");

            if (user.Status == UserStatus.Deleted)
                throw new Exception("Невозможно заблокировать удалённого пользователя.");

            var desiredStatus = isBlocked ? UserStatus.Blocked : UserStatus.Active;

            if (user.Status == desiredStatus)
                return true;

            user.Status = desiredStatus;

            await _userRepository.UpdateAsync(user, cancellationToken);
            return true;
        }
    }
}