namespace UserService.BLL.Dto
{
    public class AuthResponseDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
