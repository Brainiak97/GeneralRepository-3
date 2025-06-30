using StateService.Domain.Models;

namespace StateService.BLL.Interfaces
{
    public interface IStateService
    {
        Task<UserHealthReport> GetDailySummaryAsync(int userId);
    }
}
