using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StateService.Api.Infrastructure;
using StateService.Api.ViewModels;
using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;
using StateService.Domain.Models;

namespace StateService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController(IStateService stateService, IFoodDataProvider foodDataProvider, IMetricDataProvider metricDataProvider, IGroqProvider yandexCloudProvider) : ControllerBase
    {
        private readonly IStateService _stateService = stateService;
        private readonly IFoodDataProvider _foodDataProvider = foodDataProvider;
        private readonly IMetricDataProvider _metricDataProvider = metricDataProvider;
        private readonly IGroqProvider _yandexCloudProvider = yandexCloudProvider;

        [HttpGet("GetDailySummary")]
        public async Task<IActionResult> GetDailySummary(int userId)
        {
            if (userId == 0)
            {
                return BadRequest();
            }

            var summary = await _stateService.GetDailySummaryAsync(userId);

            return Ok(summary);
        }

        [HttpPost("GetRecommendations")]
        public async Task<IActionResult> GetRecommendations([FromBody] IEnumerable<UserHealthReport> reports)
        {
            try
            {
                var summary = MetricAggregator.AggregateHealthData(reports);
                var recommendations = await _yandexCloudProvider.GetHealthRecommendationsAsync(summary);

                return Ok(new
                {
                    summary.Period,
                    Recommendations = recommendations,
                    Summary = new
                    {
                        summary.AvgHeartRate,
                        summary.AvgBloodPressureSys,
                        summary.AvgBloodPressureDia,
                        summary.AvgBodyFatPercentage,
                        summary.AvgSleepDurationHours,
                        summary.AvgSleepQuality,
                        summary.AvgDailyWaterIntake,
                        summary.WorkoutCount,
                        summary.TotalCaloriesBurned,
                    }
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Ошибка при генерации рекомендаций", Details = ex.Message });
            }
        }
    }
}
