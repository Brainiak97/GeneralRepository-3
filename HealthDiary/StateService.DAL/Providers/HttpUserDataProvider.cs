using StateService.DAL.Interfaces;
using UserService.Domain.Models;

namespace StateService.DAL.Providers
{
    public class HttpUserDataProvider(HttpClient httpClient) : IUserDataProvider
    {
        private readonly HttpClient _httpClient = httpClient;

        public Task<User> GetUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
