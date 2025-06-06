using MetricService.BLL.Interfaces;
using MetricService.BLL.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MetricService.BLL.DTO;

namespace MetricService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("CreateProfile")]
        public async Task<IActionResult> CreateProfile([FromBody] UserDTO userDTO)
        {
            try
            {
                await _userService.CreateProfileAsync(userDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDTO userDTO)
        {
            try
            {
                await _userService.UpdateProfileAsync(userDTO);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpDelete("DeleteProfile")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            try
            {
                await _userService.DeleteProfileAsync(id);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int pagenum, int pagesize)
        {
            try
            {
                var result = await _userService.GetAllUsersAsync(pagenum, pagesize);

                if (!result.Any())
                {
                    return Ok("Список пуст");
                }

                return Ok(result.ToArray());
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserByIds(int userid)
        {
            try
            {
                return Ok(await _userService.GetUserByIdAsync(userid));
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.GetError());
            }
        }
    }
}
