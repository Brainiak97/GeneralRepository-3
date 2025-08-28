using Microsoft.AspNetCore.Mvc;
using StateService.Api.Infrastructure;
using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;
using StateService.Domain.Models;

namespace StateService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController(IStateService stateProvider, IFoodDataProvider foodDataProvider, IMetricDataProvider metricDataProvider, IGroqProvider yandexCloudProvider) : ControllerBase
    {
        private readonly IStateService _stateProvider = stateProvider;
        private readonly IFoodDataProvider _foodDataProvider = foodDataProvider;
        private readonly IMetricDataProvider _metricDataProvider = metricDataProvider;
        private readonly IGroqProvider _groqProvider = yandexCloudProvider;

        [HttpGet(nameof(GetDailySummary))]
        public async Task<IActionResult> GetDailySummary(int userId)
        {
            if (userId == 0)
            {
                return BadRequest();
            }

            var report = await _stateService.GetDailySummaryAsync(userId);

            return Ok(report);
        }

        [HttpGet(nameof(GetPeriodSummary))]
        public async Task<IActionResult> GetPeriodSummary([FromQuery] RequestListWithPeriodByIdViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.StartDate > request.EndDate)
            {
                return BadRequest("Начальная дата не может быть позже конечной.");
            }

            if (request.UserId <= 0)
            {
                return BadRequest("Некорректный идентификатор пользователя.");
            }

            try
            {
                var reports = await _stateService.GetPeriodSummaryAsync(
                    request.UserId,
                    request.StartDate,
                    request.EndDate
                );

                if (reports == null || !reports.Any())
                {
                    return Ok(new { Message = "Данные за указанный период отсутствуют." });
                }

                return Ok(reports);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Произошла ошибка при обработке запроса.");
            }
        }

        [HttpPost("GetRecommendations")]
        public async Task<IActionResult> GetRecommendations([FromBody] IEnumerable<UserHealthReport> reports)
        {
            try
            {
                var summary = MetricAggregator.AggregateHealthData(reports);
                var recommendations = await _groqProvider.GetHealthRecommendationsAsync(summary);

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
