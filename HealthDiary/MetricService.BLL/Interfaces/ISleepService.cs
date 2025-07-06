using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Sleep;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о сне пользователя
    /// </summary>
    public interface ISleepService
    {
        /// <summary>
        /// Создать запись о сне пользователя
        /// </summary>
        /// <param name="sleepDTO">Данные для создания записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>          
        public Task CreateRecordOfSleepAsync(SleepCreateDTO sleepDTO);

        /// <summary>
        /// Обновить запись о сне пользователя
        /// </summary>
        /// <param name="sleepDTO">Данные для изменения записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Сон не зарегистрирован</exception>
        public Task UpdateRecordOfSleepAsync(SleepUpdateDTO sleepDTO);

        /// <summary>
        /// Удалить запись о сне пользователя
        /// </summary>
        /// <param name="sleepId">Идентификатор записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанный пользователь не существует</exception>
        public Task DeleteRecordOfSleepAsync(int sleepId);

        /// <summary>
        /// Получить запись о сне пользователя
        /// </summary>
        /// <param name="sleepId">Идентификатор записи</param>
        /// <returns>Запись о сне пользователя</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанный пользователь не существует</exception>
        public Task<SleepDTO> GetRecordOfSleepByIdAsync(int sleepId);

        /// <summary>
        /// Получить все записи о снах пользователя за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>        
        /// <returns>Список записей о снах пользователя за период</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public Task<IEnumerable<SleepDTO>> GetAllRecordsOfSleepByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
