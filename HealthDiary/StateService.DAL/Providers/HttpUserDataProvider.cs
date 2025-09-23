using StateService.DAL.Interfaces;
using StateService.Domain.Dto;
using System.Net.Http.Json;

namespace StateService.DAL.Providers
{
    public class HttpUserDataProvider(HttpClient httpClient) : IUserDataProvider
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<UserDto?> GetUserAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/user/GetUserInfo?userId={userId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch user data");

            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
    }
}
