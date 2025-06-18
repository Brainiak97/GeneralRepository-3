using MetricService.BLL.Exceptions;
using MetricService.BLL.DTO.Intake;
using MetricService.BLL.DTO;

namespace MetricService.BLL.Interfaces
{
    public interface IIntakeService
    {
        /// <summary>
        /// Создать запись приема лекарств
        /// </summary>
        /// <param name="intakeCreateDTO">запись приема лекарств</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о приеме лекарств</exception>
        public Task CreateIntakeAsync(IntakeCreateDTO intakeCreateDTO);

        /// <summary>
        /// Обновление записи приема лекарств
        /// </summary>
        /// <param name="intakeUpdateDTO">запись приема лекарств</param>
        /// <exception cref="IncorrectOrEmptyResultException">Запись приема лекарств не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о записи приема лекарств</exception>
        public Task UpdateIntakeAsync(IntakeUpdateDTO intakeUpdateDTO);

        /// <summary>
        /// Удалить запись приема лекарств
        /// </summary>
        /// <param name="intakeId">ИД записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись приема лекарств не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено удалить только свою запись приема лекарств</exception>
        public Task DeleteIntakeAsync(int intakeId);

        /// <summary>
        /// Получить запись приема лекасртв
        /// </summary>
        /// <param name="intakeId">ИД записи</param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись приема лекарств не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои записи приема лекарств</exception>
        public Task<IntakeDTO> GetIntakeByIdAsync(int intakeId);

        /// <summary>
        /// Получить все запис приема лекарств для пользователя
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">запрос</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои записи приема лекарств</exception>
        public Task<IEnumerable<IntakeDTO>> GetAllIntakeByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
