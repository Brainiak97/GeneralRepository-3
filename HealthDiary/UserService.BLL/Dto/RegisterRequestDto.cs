using UserService.Domain.Models;

namespace UserService.BLL.Dto
{
    public class RegisterRequestDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public required string PasswordHash { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; }
        public bool IsBlocked { get; set; }
    }
}
