using StateService.DAL.Interfaces;
using StateService.Domain.Dto;
using System.Net.Http.Json;

namespace StateService.DAL.Providers
{
    public class HttpMetricDataProvider(HttpClient httpClient) : IMetricDataProvider
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<HealthMetricsDto>> GetHealthMetricsBaseDataAsync(int userId, DateTime startDate, DateTime endDate)
        {
            var url = $"/api/HealthMetricsBase/GetAllHealthMetricsValue" +
                      $"?UserId={userId}" +
                      $"&BegDate={startDate:yyyy-MM-dd}" +
                      $"&EndDate={endDate:yyyy-MM-dd}";

            var response = await _httpClient.GetFromJsonAsync<List<TempHealthMetric>>(url) ?? [];

            return [.. response.Select(t => new HealthMetricsDto
            {
                MetricDate = t.RecordedAt,
                MetricName = t.HealthMetric?.Name ?? "Unknown",
                Value = t.Value,
                Unit = t.HealthMetric?.Unit,
                Comment = t.Comment
            })];
        }

        public async Task<List<WorkoutDto>> GetWorkoutDataAsync(int userId, DateTime startDate, DateTime endDate)
        {
            var url = $"/api/workout/GetAllWorkouts" +
                      $"?UserId={userId}" +
                      $"&BegDate={startDate:yyyy-MM-dd}" +
                      $"&EndDate={endDate:yyyy-MM-dd}";

            var response = await _httpClient.GetFromJsonAsync<List<WorkoutDto>>(url);
            return response ?? [];
        }

        public async Task<List<SleepDto>> GetSleepDataAsync(int userId, DateTime startDate, DateTime endDate)
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
