using StateService.Domain.Models;

namespace StateService.BLL.Interfaces
{
    public interface IStateService
    {
        Task<UserHealthSummary> GetDailySummaryAsync(int userId);
    }
}
