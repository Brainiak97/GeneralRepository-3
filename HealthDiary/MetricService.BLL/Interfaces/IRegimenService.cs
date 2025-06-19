using MetricService.BLL.DTO.Regimen;
using MetricService.BLL.DTO;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    public interface IRegimenService
    {
        /// <summary>
        /// Создать схемк приема лекарств
        /// </summary>
        /// <param name="regimenCreateDTO">Схема приема лекарств</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о схеме приема лекарств</exception>
        public Task CreateRegimenAsync(RegimenCreateDTO regimenCreateDTO);

        /// <summary>
        /// Обновить схему приема лекарств
        /// </summary>
        /// <param name="regimenUpdateDTO">Схема приема лекарств</param>
        /// <exception cref="IncorrectOrEmptyResultException">Схема приема не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные о тренировке для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о схеме приема</exception>
        public Task UpdateRegimenAsync(RegimenUpdateDTO regimenUpdateDTO);

        /// <summary>
        ///Удалить схему приема лекарств
        /// </summary>
        /// <param name="regimenId">ИД схемы приема</param>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная схема приема не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено удалить только свою схему приема</exception>
        public Task DeleteRegimenAsync(int regimenId);

        /// <summary>
        /// Получить схему приема лекарств
        /// </summary>
        /// <param name="regimenId">ИД схемв приема лекарств</param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная схема приема не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свою тренировку</exception>
        public Task<RegimenDTO> GetRegimenByIdAsync(int regimenId);

        /// <summary>
        /// Получить схемы приема лекарств за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">запрос</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои схемы приема</exception>
        public Task<IEnumerable<RegimenDTO>> GetAllRegimenByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
