using Newtonsoft.Json;
using StateService.DAL.Interfaces;
using System.Net.Http.Json;
using UserService.Domain.Models;

namespace StateService.DAL.Providers
{
    public class HttpUserDataProvider(HttpClient httpClient) : IUserDataProvider
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<User?> GetUserAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/user/GetUserInfo?userId={userId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch user data");

            return await response.Content.ReadFromJsonAsync<User>();
        }
    }
}
