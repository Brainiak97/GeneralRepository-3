using Microsoft.AspNetCore.Mvc;
using UserService.BLL.Dto;
using UserService.BLL.Interfaces;

namespace UserService.Api.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для регистрации, авторизации, подтверждения email и восстановления пароля.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <param name="request">Данные для регистрации пользователя.</param>
        /// <returns>Результат операции регистрации.</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var response = await _userService.Register(request);
            return Ok(response);
        }

        /// <summary>
        /// Выполняет вход пользователя по логину и паролю.
        /// </summary>
        /// <param name="request">Данные для входа (логин/пароль).</param>
        /// <returns>Токен аутентификации или ошибку.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _userService.Login(request);
            return Ok(response);
        }
    }
}
