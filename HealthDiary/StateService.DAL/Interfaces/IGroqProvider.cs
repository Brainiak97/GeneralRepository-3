using StateService.Domain.Dto;
namespace StateService.DAL.Interfaces
{
    public interface IGroqProvider
    {
        Task<string> GetHealthRecommendationsAsync(AggregatedHealthSummary metrics);
    }
}
