using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.BLL.Exceptions;


namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о результате анализа пользователя
    /// </summary>
    public interface IAnalysisResultService 
    {
        /// <summary>
        /// Создать запись результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultDTO">Данные для создания записи</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о результате анализа пользователя</exception>
        public Task CreateAnalysisResultAsync(AnalysisResultCreateDTO analysisResultDTO);

        /// <summary>
        /// Обновить запись результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultDTO">Данные для изменения записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Запись об анализах не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные об анализах пользователя</exception>
        public Task UpdateAnalysisResultAsync(AnalysisResultUpdateDTO analysisResultDTO);

        /// <summary>
        /// Удалить запись результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultId">Идентификатор записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о результате анализа пользователя не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено удалять только свои записи</exception>
        public Task DeleteAnalysisResultAsync(int analysisResultId);

        /// <summary>
        /// Получить запись результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultId">Идентификатор записи</param>
        /// <returns>Запись результата анализа пользователя</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись об анализах не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои данные</exception>
        public Task<AnalysisResultDTO?> GetAnalysisResultByIdAsync(int analysisResultId);

        /// <summary>
        /// Получить список записей результатов анализов пользователя за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns>Список записей результатов анализов пользователя за период</returns>
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои анализы</exception>
        public Task<IEnumerable<AnalysisResultDTO>> GetAllAnalysisResultsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
