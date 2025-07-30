using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;

namespace StateService.Api.Controllers
{
    //[Authorize]
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

            var summary = await _stateService.GetDailySummaryAsync(userId);

            return Ok(summary);
        }
    }
}
