using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.BLL.Exceptions;


namespace MetricService.BLL.Interfaces
{
    public interface IAnalysisResultService 
    {
        /// <summary>
        /// Создание записи результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultDTO">Результат анализа</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о результате анализа пользователя</exception>
        public Task CreateAnalysisResultAsync(AnalysisResultCreateDTO analysisResultDTO);

        /// <summary>
        /// Обновление записи результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultDTO">Результат анализа</param>
        /// <exception cref="IncorrectOrEmptyResultException">Запись об анализах не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные об анализах пользователя</exception>
        public Task UpdateAnalysisResultAsync(AnalysisResultUpdateDTO analysisResultDTO);

        /// <summary>
        /// удаление записи результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultId">ИД результата анализа</param>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о результате анализа пользователя не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено удалять только свои записи</exception>
        public Task DeleteAnalysisResultAsync(int analysisResultId);

        /// <summary>
        /// получить запись результата анализа пользователя
        /// </summary>
        /// <param name="analysisResultId">ИД результата анализа</param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись об анализах не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои данные</exception>
        public Task<AnalysisResultDTO?> GetAnalysisResultByIdAsync(int analysisResultId);

        /// <summary>
        /// Получение записей результатах анализов пользователя за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Запрос</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои анализы</exception>
        public Task<IEnumerable<AnalysisResultDTO>> GetAllAnalysisResultsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
