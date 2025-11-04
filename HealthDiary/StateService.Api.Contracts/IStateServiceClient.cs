using Refit;
using StateService.Api.Contracts.Dtos;

namespace StateService.Api.Contracts
{
    public interface IStateServiceClient       
    {
        [Get($"/{nameof(GetDailySummary)}")]
        public Task<UserHealthReportDto> GetDailySummary(int userId);


        [Get($"/{nameof(GetPeriodSummary)}")]
        public Task<IEnumerable<UserHealthReportDto>> GetPeriodSummary(RequestListWithPeriodByIdDto request);


        [Post($"/{nameof(GetRecommendations)}")]
        public Task<RecomendationDto> GetRecommendations(IEnumerable<UserHealthReportDto> reports);


        [Get($"/{nameof(Test)}")]
        public Task<ProductDto?> Test(int productId);

        [Get($"/{nameof(GetMedicationProgress)}")]
        public Task<MedicationProgressDto> GetMedicationProgress(RequestListWithPeriodByIdDto request);
    }
}
