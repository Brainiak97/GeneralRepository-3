using UserService.BLL.Dto;
using UserService.Domain.Models;

namespace UserService.BLL.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponseDto> Register(RegisterRequestDto request);
        Task<AuthResponseDto> Login(LoginRequestDto request);
        Task<UserDto> FindByEmail(string email);
        Task ConfirmEmailAsync(string email);
        Task ResetPassword(string email, string newPassword);
    }
}
