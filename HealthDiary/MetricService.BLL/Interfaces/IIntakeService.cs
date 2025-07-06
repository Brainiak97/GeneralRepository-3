using MetricService.BLL.Exceptions;
using MetricService.BLL.DTO.Intake;
using MetricService.BLL.DTO;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о приеме медикаментов пользователем
    /// </summary>
    public interface IIntakeService
    {
        /// <summary>
        /// Создать запись приема медикаментов пользователя
        /// </summary>
        /// <param name="intakeCreateDTO">Данные для создания записи</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о приеме лекарств</exception>
        public Task CreateIntakeAsync(IntakeCreateDTO intakeCreateDTO);

        /// <summary>
        /// Обновить запись приема медикаментов пользователя
        /// </summary>
        /// <param name="intakeUpdateDTO">Данные для изменения записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Запись приема лекарств не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о записи приема лекарств</exception>
        public Task UpdateIntakeAsync(IntakeUpdateDTO intakeUpdateDTO);

        /// <summary>
        /// Удалить запись приема медикаментов пользователя
        /// </summary>
        /// <param name="intakeId">Идентификатор записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись приема лекарств не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено удалить только свою запись приема лекарств</exception>
        public Task DeleteIntakeAsync(int intakeId);

        /// <summary>
        /// Получить запис приема медикаментов пользователем
        /// </summary>
        /// <param name="intakeId">Идентификатор записи</param>
        /// <returns>Данные записи приема медикаментов</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись приема лекарств не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои записи приема лекарств</exception>
        public Task<IntakeDTO> GetIntakeByIdAsync(int intakeId);

        /// <summary>
        /// Получить списка записей приема медикаментов для пользователя за преиод
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns>Список записей приема медикаментов для пользователя за преиод</returns>
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои записи приема лекарств</exception>
        public Task<IEnumerable<IntakeDTO>> GetAllIntakeByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
