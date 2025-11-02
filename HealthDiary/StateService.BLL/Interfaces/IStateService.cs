using StateService.Api.Contracts.Dtos;
using StateService.Domain.Dto;
using StateService.Domain.Models;

namespace StateService.BLL.Interfaces
{
    public interface IStateService
    {
        Task<UserHealthReport> GetDailySummaryAsync(int userId);
        Task<IEnumerable<UserHealthReport>> GetPeriodSummaryAsync(RequestListWithPeriodById request);
    }
}
