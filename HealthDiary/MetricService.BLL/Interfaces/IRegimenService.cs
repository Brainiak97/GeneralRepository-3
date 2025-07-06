using MetricService.BLL.DTO.Regimen;
using MetricService.BLL.DTO;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о схеме приема медикаментов пользователем
    /// </summary>
    public interface IRegimenService
    {
        /// <summary>
        /// Создать запись схемы приема медикаментов
        /// </summary>
        /// <param name="regimenCreateDTO">Данные для создания записи</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о схеме приема лекарств</exception>
        public Task CreateRegimenAsync(RegimenCreateDTO regimenCreateDTO);

        /// <summary>
        /// Обновить запись схемы приема медикаментов
        /// </summary>
        /// <param name="regimenUpdateDTO">Данные для изменения записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Схема приема не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные о тренировке для других пользователей</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о схеме приема</exception>
        public Task UpdateRegimenAsync(RegimenUpdateDTO regimenUpdateDTO);

        /// <summary>
        /// Удалить запись схемы приема медикаментов
        /// </summary>
        /// <param name="regimenId">Идентификатор записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная схема приема не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено удалить только свою схему приема</exception>
        public Task DeleteRegimenAsync(int regimenId);

        /// <summary>
        /// Получить запись схемы приема медикаментов
        /// </summary>
        /// <param name="regimenId">Идентификатор записи</param>
        /// <returns>Запись схемы приема медикаментов</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная схема приема не существует</exception>        
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свою тренировку</exception>
        public Task<RegimenDTO> GetRegimenByIdAsync(int regimenId);

        /// <summary>
        /// Получить список записей схемы приема медикаментов пользователем за период
        /// </summary>
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns>Список записей схемы приема медикаментов пользователем за период</returns>
        /// <exception cref="ViolationAccessException">Вам разрешено просматривать только свои схемы приема</exception>
        public Task<IEnumerable<RegimenDTO>> GetAllRegimenByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);
    }
}
