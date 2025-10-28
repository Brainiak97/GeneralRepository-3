using MetricService.Api.Contracts.Dtos.AnalysisType;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с данными справочника "Типы анализов"
    /// </summary>
    public interface IAnalysisTypeServiceClient
    {
        const string Controller = "/AnalysisType";
        /// <summary>
        /// Зарегистрировать новый тип анализов в справочнике "Типы анализов"
        /// </summary>
        /// <param name="analysisTypeCreateDTO">Данные о новом типе анализов для регистрации</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateAnalysisType)}")]
        Task CreateAnalysisType(AnalysisTypeCreateDTO analysisTypeCreateDTO);

        /// <summary>
        /// Изменить данные о типе анализов в справочнике "Типы анализов"
        /// </summary>
        /// <param name="analysisTypeUpdateDTO">Данные об изменении типа анализов</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateAnalysisType)}")]
        Task UpdateAnalysisType(AnalysisTypeUpdateDTO analysisTypeUpdateDTO);

        /// <summary>
        /// Удалить данные о типе анализов из справочника "Типы анализов"
        /// </summary>
        /// <param name="analysisTypeId">Идентификатор типа анализов</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteAnalysisType)}")]
        Task DeleteAnalysisType(int analysisTypeId);

        /// <summary>
        /// Получить список типов анализов из справочника "Типы анализов"
        /// </summary>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllAnalysisTypes)}")]
        Task<IEnumerable<AnalysisTypeDTO>> GetAllAnalysisTypes();

        /// <summary>
        /// Получить тип анализа из справочника "Типы анализов"
        /// </summary>
        /// <param name="analysisTypeId">Идентификатор типа анализа</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAnalysisTypeById)}")]
        Task<AnalysisTypeDTO> GetAnalysisTypeById(int analysisTypeId);

        /// <summary>
        /// Поучить из справочника "Типы анализов" все подходящие строки, заданные критерием
        /// </summary>
        /// <param name="search">Строка поиска. Для множественного поиска фразы разделять запятой</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(FindAnalysisTypeByName)}")]
        Task<IEnumerable<AnalysisTypeDTO>> FindAnalysisTypeByName(string search);

    }
}
