using UserService.BLL.Dto;

namespace UserService.BLL.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponseDto> Register(RegisterRequestDto request);
        Task<AuthResponseDto> Login(LoginRequestDto request);
    }
}
