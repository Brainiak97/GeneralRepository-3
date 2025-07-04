using MetricService.BLL.DTO.DosageForm;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    public  interface IDosageFormService
    {
        /// <summary>
        /// создает форму выпуска препарата
        /// </summary>
        /// <param name="dosageFormCreateDTO">Форма выпуска</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные</exception>        
        public Task CreateDosageFormAsync(DosageFormCreateDTO dosageFormCreateDTO);

        /// <summary>
        /// Обновить данные формы выпуска
        /// </summary>
        /// <param name="dosageFormUpdateDTO">Форма выпуска</param>
        /// <exception cref="IncorrectOrEmptyResultException">Форма выпуска не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные</exception>        
        public Task UpdateDosageFormAsync(DosageFormUpdateDTO dosageFormUpdateDTO);

        /// <summary>
        /// удаляет форму выпуска препарата
        /// </summary>
        /// <param name="dosageFormId">ИД формы выпуска препарата</param>
        /// <exception cref="IncorrectOrEmptyResultException">Форма выпуска не зарегистрирована</exception>        
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные</exception>
        public Task DeleteDosageFormAsync(int dosageFormId);

        /// <summary>
        /// Получить форму выпуска
        /// </summary>
        /// <param name="dosageFormId">ИД формы выпуска</param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Форма выпуска не существует</exception>   
        public Task<DosageFormDTO> GetDosageFormByIdAsync(int dosageFormId);

        /// <summary>
        /// Получить список форм выпуска
        /// </summary>        
        /// <returns></returns>
        public Task<IEnumerable<DosageFormDTO>> GetAllDosageFormsAsync();
    }
}
