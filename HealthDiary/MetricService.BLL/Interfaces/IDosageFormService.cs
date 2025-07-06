using MetricService.BLL.DTO.DosageForm;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными справочника "Форма выпуска препарата"
    /// </summary>
    public interface IDosageFormService
    {
        /// <summary>
        /// Создать запись в справочнике
        /// </summary>
        /// <param name="dosageFormCreateDTO">Данные для создания записи</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные</exception>        
        public Task CreateDosageFormAsync(DosageFormCreateDTO dosageFormCreateDTO);

        /// <summary>
        /// Изменить запись в справочнике
        /// </summary>
        /// <param name="dosageFormUpdateDTO">Данные для изменения записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Форма выпуска не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные</exception>        
        public Task UpdateDosageFormAsync(DosageFormUpdateDTO dosageFormUpdateDTO);

        /// <summary>
        /// Удалить запись в справочнике
        /// </summary>
        /// <param name="dosageFormId">Идентификатор записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Форма выпуска не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные</exception>
        public Task DeleteDosageFormAsync(int dosageFormId);

        /// <summary>
        /// Получить запись из справочника
        /// </summary>
        /// <param name="dosageFormId">Идентификатор записи</param>
        /// <returns>Запись из справочника</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Форма выпуска не существует</exception>   
        public Task<DosageFormDTO> GetDosageFormByIdAsync(int dosageFormId);

        /// <summary>
        /// Получить список записей из справочника
        /// </summary>        
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<DosageFormDTO>> GetAllDosageFormsAsync();
    }
}
