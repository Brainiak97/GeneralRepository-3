namespace UserService.Api.Contracts.Dtos
{
    public record UserSessionDto(string Role, DateTime LastActivity);
}
