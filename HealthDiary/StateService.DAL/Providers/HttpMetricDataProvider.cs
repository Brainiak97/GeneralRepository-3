using MetricService.Domain.Models;
using StateService.DAL.Interfaces;

namespace StateService.DAL.Providers
{
    public class HttpMetricDataProvider(HttpClient httpClient) : IMetricDataProvider
    {
        private readonly HttpClient _httpClient = httpClient;

        public Task<HealthMetricValue> GetHealthMetricsBaseDataAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<PhysicalActivity> GetPhysicalActivityDataAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Sleep> GetSleepDataAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Workout> GetWorkoutDataAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
