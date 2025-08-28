using StateService.DAL.Interfaces;
using StateService.Domain.Dto;
using System.Net.Http.Json;

namespace StateService.DAL.Providers
{
    public class HttpMetricDataProvider(HttpClient httpClient) : IMetricDataProvider
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<HealthMetricsDto>> GetHealthMetricsBaseDataAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var url = $"/api/HealthMetricsBase/GetAllRecordsOfHealthMetricsBase" +
                      $"?UserId={userId}" +
                      $"&BegDate={startDate:yyyy-MM-dd}" +
                      $"&EndDate={endDate:yyyy-MM-dd}";

            var response = await _httpClient.GetFromJsonAsync<List<HealthMetricsDto>>(url);
            return response ?? [];
        }

        public async Task<List<WorkoutDto>> GetWorkoutDataAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var url = $"/api/workout/GetAllWorkouts" +
                      $"?UserId={userId}" +
                      $"&BegDate={startDate:yyyy-MM-dd}" +
                      $"&EndDate={endDate:yyyy-MM-dd}";

            var response = await _httpClient.GetFromJsonAsync<List<WorkoutDto>>(url);
            return response ?? [];
        }

        public async Task<List<SleepDto>> GetSleepDataAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var url = $"/api/sleep/GetAllSleeps" +
                      $"?UserId={userId}" +
                      $"&BegDate={startDate:yyyy-MM-dd}" +
                      $"&EndDate={endDate:yyyy-MM-dd}";

            var response = await _httpClient.GetFromJsonAsync<List<SleepDto>>(url);
            return response ?? [];
        }
    }
}
