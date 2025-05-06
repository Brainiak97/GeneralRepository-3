using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserService.BLL.Dto;
using UserService.BLL.Interfaces;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var response = await _userService.Register(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _userService.Login(request);
            return Ok(response);
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            // Только пользователи с ролью "Admin" могут получить доступ
            return Ok(new { Message = "Доступ разрешён для администраторов" });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);

            return Ok(new { UserId = userId, Roles = roles });
        }
    }
}
