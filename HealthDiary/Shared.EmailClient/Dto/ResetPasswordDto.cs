namespace Shared.EmailClient.Dto
{
    public class ResetPasswordDto
    {
        public required string Token { get; set; }
        public required string NewPassword { get; set; }
    }
}
