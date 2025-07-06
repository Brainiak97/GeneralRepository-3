using MetricService.BLL.DTO.Reminder;
using MetricService.BLL.DTO;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о напоминании приема медикаментов пользователем
    /// </summary>
    public interface IReminderService
    {
        /// <summary>
        /// Создать запись напоминания приема медикаментов пользователем
        /// </summary>
        /// <param name="reminderCreateDTO">Данные для создания записи</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о напоминании</exception>
        public Task CreateReminderAsync(ReminderCreateDTO reminderCreateDTO);

        /// <summary>
        /// Обновить запись напоминания приема медикаментов пользователем
        /// </summary>
        /// <param name="reminderUpdateDTO">Данные для изменения записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Напоминание не зарегистрировано</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные о тренировке для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о напоминании</exception>
        public Task UpdateReminderAsync(ReminderUpdateDTO reminderUpdateDTO);

        /// <summary>
        /// Удалить запись напоминания приема медикаментов пользователем
        /// </summary>
        /// <param name="reminderId">Идентификатор записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Напоминание не зарегистрировано</exception>        
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные - 0</exception>
        public Task DeleteReminderAsync(int reminderId);

        /// <summary>
        /// Получить запись напоминания приема медикаментов пользователем
        /// </summary>
        /// <param name="reminderId">Идентификатор записи</param>
        /// <returns>Данные о напоминании приема медикаментов пользователем</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанное напоминание не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свою тренировку</exception>
        public Task<ReminderDTO> GetReminderByIdAsync(int reminderId);

        /// <summary>
        /// Получить список записей напоминаний приема медикаментов пользователем за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns>Список записей напоминаний приема медикаментов пользователем за периодм</returns>
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои напоминания</exception>
        public Task<IEnumerable<ReminderDTO>> GetAllReminderByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);

        /// <summary>
        /// Получить список записей напоминаний по схеме приема за период
        /// </summary>
        /// <param name="requestListWithPeriodByRegimenIdDTO">Данные схемы приема медикаментов и период</param>
        /// <returns>Список данных о напоминаниях приема медикаментов пользователем по схеме приема медаментов за период</returns>
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои напоминания</exception>
        public Task<IEnumerable<ReminderDTO>> GetAllReminderByRegimenIdAsync(RequestListWithPeriodByRegimenIdDTO requestListWithPeriodByRegimenIdDTO);
    }
}
