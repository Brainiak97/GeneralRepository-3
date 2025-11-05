using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.BLL.Dto;
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
        /// Регистрирует нового пользователя.
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
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="roleName">Данные для регистрации пользователя.</param>
        /// <param name="userId">Данные для регистрации пользователя.</param>
        /// <returns>Результат операции регистрации.</returns>
        [HttpPost("AssignRoleToUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] string roleName, int userId, CancellationToken cancellationToken)
        {
            await _userService.AssignRoleToUser(roleName, userId, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <returns>Результат операции регистрации.</returns>
        [HttpPost("GetUsersActivity")]
        public IActionResult GetUsersActivity()
        {
            return Ok(
                new UsersActivity
                {
                    DoctorsLoggedIn = _onlineUsersService.GetDoctorCount(),
                    PatientsLoggedIn = _onlineUsersService.GetPatientCount(),
                    Total = _onlineUsersService.GetDoctorCount() + _onlineUsersService.GetPatientCount()
                });
        }
    }
}
