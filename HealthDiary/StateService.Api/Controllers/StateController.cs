using AutoMapper;
using FoodService.Api.Contracts;
using MetricService.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using StateService.Api.Contracts.Dtos;
using StateService.Api.Infrastructure;
using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;
using StateService.Domain.Dto;

namespace StateService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController(IStateService stateService,
                                 IGroqProvider groqProvider,
                                 IFoodServiceClient foodServiceClient,
                                 IMapper mapper,
                                 IMetricServiceClient metricServiceClient) : ControllerBase
    {
        private readonly IStateService _stateService = stateService;
        private readonly IGroqProvider _groqProvider = groqProvider;
        private readonly IFoodServiceClient _foodServiceClient = foodServiceClient;
        private readonly IMapper _mapper = mapper;
        private readonly IMetricServiceClient _metricServiceClient = metricServiceClient;


        [HttpGet(nameof(GetDailySummary))]
        public async Task<IActionResult> GetDailySummary(int userId)
        {
            if (userId == 0)
            {
                return BadRequest();
            }

            var report = await _stateService.GetDailySummaryAsync(userId);
            var result = _mapper.Map<UserHealthReportDto>(report);
            return Ok(result);
        }

        [HttpGet(nameof(GetPeriodSummary))]
        public async Task<IActionResult> GetPeriodSummary([FromQuery] RequestListWithPeriodByIdDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.BegDate > request.EndDate)
            {
                return BadRequest($"Начальная дата ({request.BegDate}) не может быть позже конечной ({request.EndDate}).");
            }

            try
            {
                var requestPeriod = _mapper.Map<RequestListWithPeriodById>(request);
                var reports = await _stateService.GetPeriodSummaryAsync(requestPeriod);

                if (reports == null || !reports.Any())
                {
                    return Ok(new { Message = $"Данные пользователя ({request.UserId}) за указанный период ({request.BegDate}-{request.EndDate}) отсутствуют." });
                }

                return Ok(_mapper.Map<List<UserHealthReportDto>>(reports));
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
        public async Task<IActionResult> GetRecommendations([FromBody] IEnumerable<UserHealthReportDto> reports)
        {
            try
            {
                var summaryDto = MetricAggregator.AggregateHealthData(reports);
                var summary = _mapper.Map<AggregatedHealthSummary>(summaryDto);

                var recommendations = await _groqProvider.GetHealthRecommendationsAsync(summary);


                return Ok(new RecomendationDto()
                {
                    Period = summary.Period,
                    Recomendation = recommendations,
                    Summary = new RecomendationSummaryDto
                    {
                        HealthMetrics = _mapper.Map<List<HealthMetricsDto>>(summary.HealthMetrics),
                        AvgSleepDurationHours = summary.AvgSleepDurationHours,
                        AvgSleepQuality = summary.AvgSleepQuality,
                        WorkoutCount = summary.WorkoutCount,
                        TotalCaloriesBurned = summary.TotalCaloriesBurned
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
    }
}
