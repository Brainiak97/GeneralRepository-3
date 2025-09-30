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
    public class StateController(IStateService stateService, IGroqProvider groqProvider) : ControllerBase
    {
        private readonly IStateService _stateService = stateService;
        private readonly IGroqProvider _groqProvider = groqProvider;

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
                return BadRequest($"Начальная дата ({request.StartDate}) не может быть позже конечной ({request.EndDate}).");
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
                    return Ok(new { Message = $"Данные пользователя ({request.UserId}) за указанный период ({request.StartDate}-{request.EndDate}) отсутствуют." });
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

        [HttpPost(nameof(GetRecommendations))]
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
