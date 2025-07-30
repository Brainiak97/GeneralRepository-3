using UserService.Domain.Models;

namespace StateService.DAL.Interfaces
{
    public interface IUserDataProvider
    {
        Task<User?> GetUserAsync(string userId);
    }
}
