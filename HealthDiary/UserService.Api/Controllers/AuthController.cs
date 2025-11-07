using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Contracts.Dtos;
using UserService.BLL.Interfaces;
using UserService.BLL.Services;

namespace UserService.Api.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для регистрации, авторизации, подтверждения email и восстановления пароля.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserService userService, OnlineUsersService onlineUsersService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly OnlineUsersService _onlineUsersService = onlineUsersService;

        /// <summary>
        /// Выполняет вход пользователя по логину и паролю.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="request">Данные для входа (логин/пароль).</param>
        /// <returns>Токен аутентификации или ошибку.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _userService.Login(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="request">Данные для регистрации пользователя.</param>
        /// <returns>Результат операции регистрации.</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _userService.Register(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Регистрирует нового доктора.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="request">Данные для регистрации пользователя.</param>
        /// <returns>Результат операции регистрации.</returns>
        [HttpPost("RegisterDoctor")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _userService.Register(request, cancellationToken, true);
            return Ok(response);
        }

        /// <summary>
        /// Добавляет пользовтаеля к докторам.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="roleName">Наименование роли.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Результат операции регистрации.</returns>
        [HttpPost("AssignRoleToUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] string roleName, int userId, CancellationToken cancellationToken)
        {
            await _userService.AssignRoleToUser(roleName, userId, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Получает активность всех пользователей.
        /// </summary>
        /// <returns>Результат операции регистрации.</returns>
        [HttpPost("GetUsersActivity")]
        public IActionResult GetUsersActivity()
        {
            var doctorsCount = _onlineUsersService.GetDoctorCount();
            var patientsCount = _onlineUsersService.GetPatientCount();
            var response = new UsersActivity
            {
                DoctorsLoggedIn = doctorsCount,
                PatientsLoggedIn = patientsCount,
                Total = doctorsCount + patientsCount
            };
            return Ok(response);
        }
    }
}
