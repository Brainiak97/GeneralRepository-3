using StateService.Domain.Dto;

namespace StateService.DAL.Interfaces
{
    public interface IUserDataProvider
    {
        Task<UserDto?> GetUserAsync(string userId);
    }
}
