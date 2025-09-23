using Microsoft.AspNetCore.Mvc;
using StateService.Api.ViewModels;
using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;

namespace StateService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController(IStateService stateService, IFoodDataProvider foodDataProvider, IMetricDataProvider metricDataProvider) : ControllerBase
    {
        private readonly IStateService _stateService = stateService;
        private readonly IFoodDataProvider _foodDataProvider = foodDataProvider;
        private readonly IMetricDataProvider _metricDataProvider = metricDataProvider;

        [HttpGet("GetDailySummary")]
        public async Task<IActionResult> GetDailySummary(int userId)
        {
            if (userId == 0)
            {
                return BadRequest();
            }

            var report = await _stateService.GetDailySummaryAsync(userId);

            return Ok(report);
        }

        [HttpGet("GetPeriodSummary")]
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
    }
}
