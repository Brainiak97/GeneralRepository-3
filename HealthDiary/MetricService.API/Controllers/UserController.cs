using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MetricService.BLL.DTO;

namespace MetricService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost(nameof(CreateProfile))]
        public async Task<IActionResult> CreateProfile([FromBody] UserDTO userDTO)
        {
            await _userService.CreateProfileAsync(userDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateProfile))]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDTO userDTO)
        {
            await _userService.UpdateProfileAsync(userDTO);
            return Ok();
        }

        [HttpDelete(nameof(DeleteProfile))]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            await _userService.DeleteProfileAsync(id);
            return Ok();
        }

        [HttpGet(nameof(GetAllUsers))]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result);
        }

        [HttpGet(nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int userid)
        {
            return Ok(await _userService.GetUserByIdAsync(userid));
        }
    }
}
