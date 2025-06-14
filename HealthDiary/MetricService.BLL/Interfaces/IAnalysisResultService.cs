using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisResult;

namespace MetricService.BLL.Interfaces
{
    public interface IAnalysisResultService 
    {
        /// <summary>
        /// Создание записи результата анализа пользователя
        /// </summary>
        /// <param name="AnalysisResultDTO">результат анализа</param>
        /// <returns></returns>
        public Task CreateAnalysisResultAsync(AnalysisResultCreateDTO analysisResultDTO);

        /// <summary>
        /// Обновление записи результата анализа пользователя
        /// </summary>
        /// <param name="AnalysisResultDTO">результат анализв</param>
        /// <returns></returns>
        public Task UpdateAnalysisResultAsync(AnalysisResultUpdateDTO analysisResultDTO);

        /// <summary>
        /// удаление записи результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultId">ИД результата анализа</param>
        /// <returns></returns>
        public Task DeleteAnalysisResultAsync(int analysisResultId);

        /// <summary>
        /// получить запись результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultId"></param>
        /// <returns></returns>
        public Task<AnalysisResultDTO?> GetAnalysisResultByIdAsync(int analysisResultId);

        /// <summary>
        /// Получение записей результатах анализов пользователя за период
        /// </summary>
        /// <param cref="RequestListWithPeriodByIdDTO">Запрос</param>        
        /// <returns></returns>
        public Task<IEnumerable<AnalysisResultDTO>> GetAllAnalysisResultsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
