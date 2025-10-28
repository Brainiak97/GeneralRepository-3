using MetricService.Api.Contracts.Dtos.AnalysisCategory;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с данными справочника "Категории анализов"
    /// </summary>
    public interface IAnalysisCategoryServiceClient
    {
        const string Controller = "/AnalysisCategory";
        /// <summary>
        /// Зарегистрировать новую категорию анализов в справочнике "Категории анализов"
        /// </summary>
        /// <param name="analysisCategoryCreateDTO">Данные для регистрации новой категории анализов</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateAnalysisCategory)}")]
        Task CreateAnalysisCategory(AnalysisCategoryCreateDTO analysisCategoryCreateDTO);

        /// <summary>
        /// Изменить данные категории анализов в справочнике "Категории анализов"
        /// </summary>
        /// <param name="analysisCategoryUpdateDTO">Данные для изменения категории анализов</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateAnalysisCategory)}")]

        Task UpdateAnalysisCategory(AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO);

        /// <summary>
        /// Удалить категорию анализов из справочника "Категории анализов"
        /// </summary>
        /// <param name="analysisCategoryId">Идентификатор категории анализов</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteAnalysisCategory)}")]
        Task DeleteAnalysisCategory(int analysisCategoryId);

        /// <summary>
        /// Получить список категорий анализов из справочника "Категории анализов"
        /// </summary>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllAnalysisCategories)}")]
        Task<IEnumerable<AnalysisCategoryDTO>> GetAllAnalysisCategories();

        /// <summary>
        /// Получить категорию анализов из справочника "Категории анализов"
        /// </summary>
        /// <param name="analysisCategoryId">Идентификатор категории анализов</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAnalysisCategoryById)}")]
        Task<AnalysisCategoryDTO> GetAnalysisCategoryById(int analysisCategoryId);

        /// <summary>
        /// Поучить из справочника "Категории анализов" все подходящие строки, заданные критерием
        /// </summary>
        /// <param name="search">Строка поиска. Для множественного поиска фразы разделять запятой</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(FindAnalysisCategoryByName)}")]
        Task<IEnumerable<AnalysisCategoryDTO>> FindAnalysisCategoryByName(string search);
    }
}
