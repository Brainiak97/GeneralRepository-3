using MetricService.Api.Contracts.Dtos.AnalysisResult;
using MetricService.Api.Contracts.Dtos.Common;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с данными результатов анализов пользователя
    /// </summary>
    public interface IAnalysisResultServiceClient
    {
        const string Controller = "/AnalysisResult";
        /// <summary>
        /// Зарегистрировать данные анализа пользователя
        /// </summary>
        /// <param name="createDTO">Данные анализа пользователя для регистрации</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateAnalysisResult)}")]
        Task CreateAnalysisResult(AnalysisResultCreateDTO createDTO);

        /// <summary>
        /// Изменить данные анализа пользователя
        /// </summary>
        /// <param name="updateDTO">Измененные данные анализа пользователя</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateAnalysisResult)}")]
        Task UpdateAnalysisResult(AnalysisResultUpdateDTO updateDTO);

        /// <summary>
        /// Удалить данные анализа пользователя
        /// </summary>
        /// <param name="analysisResultId">Идентификатор данных анализа пользователя</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteAnalysisResult)}")]
        Task DeleteAnalysisResult(int analysisResultId);

        /// <summary>
        /// Получить список анализов пользователя за период
        /// </summary>
        /// <param name="requestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllAnalysisResults)}")]
        Task<IEnumerable<AnalysisResultDTO>> GetAllAnalysisResults(RequestListWithPeriodByIdDTO requestDTO);

        /// <summary>
        /// Получить данные анализа пользователя
        /// </summary>
        /// <param name="analysisResultId">Идентификатор данные анализа пользователя</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAnalysisResultById)}")]
        Task<AnalysisResultDTO> GetAnalysisResultById(int analysisResultId);
    }
}
