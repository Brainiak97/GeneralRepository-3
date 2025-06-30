using StateService.DAL.Interfaces;

namespace StateService.DAL.Providers
{
    public class HttpFoodDataProvider(HttpClient httpClient) : IFoodDataProvider
    {
        private readonly HttpClient _httpClient = httpClient;

        public Task<object> GetFoodDataAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
