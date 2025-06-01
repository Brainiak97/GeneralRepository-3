using MetricService.BLL.Dto;
using MetricService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetricService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("SaveProfile")]
        public async Task<IActionResult> SaveProfile([FromBody] UserDTO userDTO)
        {
            var user = await _userService.GetUserByIdAsync(userDTO.Id);
            if (user == null)
            {
                return Ok(await _userService.CreateProfileAsync(userDTO));
            }
            else
            {
                return Ok(await _userService.UpdateProfileAsync(userDTO));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var responce = await _userService.DeleteProfileAsync(id);
            if (responce)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int pagenum, int pagesize)
        {
            var result = await _userService.GetAllUsersAsync(pagenum, pagesize);

            if (!result.Any())
            {
                return Ok("Список пуст");
            }

            return Ok(result.ToArray());
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserByIds(int userid)
        {
            var result = await _userService.GetUserByIdAsync(userid);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
