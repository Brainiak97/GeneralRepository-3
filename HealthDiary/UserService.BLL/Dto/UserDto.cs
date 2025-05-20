namespace UserService.BLL.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
