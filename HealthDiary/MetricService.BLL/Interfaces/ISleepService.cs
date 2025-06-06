using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Sleep;

namespace MetricService.BLL.Interfaces
{
    public  interface ISleepService
    {
        /// <summary>
        /// Создание информации о сне
        /// </summary>
        /// <param name="sleepDTO">Сон</param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>       
        public Task CreateRecordOfSleepAsync(SleepCreateDTO sleepDTO);

        /// <summary>
        /// Обновление информации о сне
        /// </summary>
        /// <param name="sleepDTO">сон</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Сон не зарегистрирован</exception>
        public Task UpdateRecordOfSleepAsync(SleepUpdateDTO sleepDTO);

        /// <summary>
        /// Удаление информации о сне пользователя
        /// </summary>
        /// <param name="sleepId">ИД сна</param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанный пользователь не существует</exception>
        public Task DeleteRecordOfSleepAsync(int sleepId);

        /// <summary>
        /// Получить информацию о сне
        /// </summary>
        /// <param name="sleepId">ИД сна</param>
        /// <returns>Модель DTO</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанный пользователь не существует</exception>
        public Task<SleepDTO> GetRecordOfSleepByIdAsync(int sleepId);

        /// <summary>
        /// Получить все записи о снах пользователя
        /// </summary>
        /// <param cref="RequestListWithPeriodByIdDTO">Запрос</param>        
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<SleepDTO>> GetAllRecordsOfSleepByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
