using MetricService.BLL.DTO.MedicationDTO;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными справочника "Медикаменты"
    /// </summary>
    public interface IMedicationService
    {

        /// <summary>
        /// Создать запись в справочнике
        /// </summary>
        /// <param name="medicationCreateDTO">Данные для создания записи</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные</exception>        
        public Task CreateMedicationAsync(MedicationCreateDTO medicationCreateDTO);

        /// <summary>
        /// Обновить запись в справочнике
        /// </summary>
        /// <param name="medicationUpdateDTO">Данные об изменении записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Лекарство не зарегистрировано</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные</exception>        
        public Task UpdateMedicationAsync(MedicationUpdateDTO medicationUpdateDTO);

        /// <summary>
        /// Удалить запись в справочнике
        /// </summary>
        /// <param name="medicationId">Идентификатор записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Лекарство не зарегистрировано</exception>        
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные</exception>
        public Task DeleteMedicationAsync(int medicationId);

        /// <summary>
        /// Получить запись из справочника
        /// </summary>
        /// <param name="medicationId">Идентификатор записи</param>
        /// <returns>Данные о медикаменте</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанное лекарство не существует</exception> 
        public Task<MedicationDTO> GetMedicationByIdAsync(int medicationId);

        /// <summary>
        ///Получение списка записей из справочника
        /// </summary>        
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<MedicationDTO>> GetAllMedicationAsync();
    }
}
