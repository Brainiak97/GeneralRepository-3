using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MetricService.BLL.DTO;

namespace MetricService.Api.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для работы с данные о профиле пользователя системы
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Зарегистрировать новый профиль пользователя
        /// </summary>
        /// <param name="userDTO">Данные для регистрации профиля пользователя</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateProfile))]
        public async Task<IActionResult> CreateProfile([FromBody] UserDTO userDTO)
        {
            await _userService.CreateProfileAsync(userDTO);
            return Ok();
        }

        /// <summary>
        /// Изменить данные профиля пользователя
        /// </summary>
        /// <param name="userDTO">Измененные данные профиля пользователя</param>
        /// <returns></returns>
        [HttpPut(nameof(UpdateProfile))]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDTO userDTO)
        {
            await _userService.UpdateProfileAsync(userDTO);
            return Ok();
        }

        /// <summary>
        /// Удалить профиль пользователя и все его данные из системы
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteProfile))]
        public async Task<IActionResult> DeleteProfile(int userId)
        {
            await _userService.DeleteProfileAsync(userId);
            return Ok();
        }

        /// <summary>
        /// Получить профиль пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet(nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int userId)
        {
            return Ok(await _userService.GetUserByIdAsync(userId));
        }
    }
}
