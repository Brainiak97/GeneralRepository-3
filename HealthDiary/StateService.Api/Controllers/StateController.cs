using FoodService.Api.Contracts;
using MetricService.Api.Contracts;
using MetricService.Api.Contracts.Dtos.Common;
using MetricService.Api.Contracts.Dtos.Intake;
using Microsoft.AspNetCore.Mvc;
using StateService.Api.Infrastructure;
using StateService.Api.ViewModels;
using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;
using StateService.Domain.Dto;
using StateService.Domain.Models;

namespace StateService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController(IStateService stateService,
                                 IGroqProvider groqProvider,
                                 IFoodServiceClient foodServiceClient,
                                 IMetricServiceClient metricServiceClient) : ControllerBase
    {
        private readonly IStateService _stateService = stateService;
        private readonly IGroqProvider _groqProvider = groqProvider;
        private readonly IFoodServiceClient _foodServiceClient = foodServiceClient;
        private readonly IMetricServiceClient _metricServiceClient = metricServiceClient;

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
                        summary.HealthMetrics,
                        summary.AvgSleepDurationHours,
                        summary.AvgSleepQuality,
                        summary.WorkoutCount,
                        summary.TotalCaloriesBurned
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

        [HttpGet(nameof(Test))]
        public async Task<IActionResult> Test(int productId)
        {
            var product = await _foodServiceClient.GetProduct(productId);
            return Ok(product);
        }

        [HttpGet(nameof(GetMedicationProgress))]
        public async Task<MedicationProgressDto> GetMedicationProgress([FromQuery] RequestListWithPeriodByIdDTO request)
        {
            //Соберем все схемы приема лекарств по пользователю, где наша дата входит в периодл приема лекарств
            var regimens = await _metricServiceClient.GetAllRegimens(request);
            //соберем все приемы лекарств
            var intakes = await _metricServiceClient.GetAllIntakes(request);
            var regimensProgress = new List<RegimenProgressDTO>();
            foreach (var regimen in regimens)
            {
                var tempRegimenProgress = new RegimenProgressDTO()
                {
                    Id = regimen.Id,
                    Comment = regimen.Comment,
                    Dosage = regimen.Dosage,
                    Shedule = regimen.Shedule,
                    EndDate = regimen.EndDate,
                    MedicationId = regimen.MedicationId,
                    StartDate = regimen.StartDate,
                    UserId = regimen.UserId,
                    Intakes = new List<IntakeDTO>()
                };

                foreach (var intake in intakes.Where(i => i.RegimenId == regimen.Id))
                {
                    tempRegimenProgress.Intakes.Add(
                        new IntakeDTO()
                        {
                            Comment = intake.Comment,
                            Id = intake.Id,
                            IntakeStatus = intake.IntakeStatus,
                            RegimenId = regimen.Id,
                            TakenAt = intake.TakenAt
                        }
                    );
                }

                regimensProgress.Add( tempRegimenProgress );
            }
            return new MedicationProgressDto()
            {
                UserId = regimens.FirstOrDefault()?.UserId ?? 0,
                Regimens = new List<RegimenProgressDTO>(regimensProgress)
            };

        }
    }
}
