using MetricService.BLL.DTO.MedicationDTO;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    public interface IMedicationService
    {

        /// <summary>
        /// Создать лекарство
        /// </summary>
        /// <param name="medicationCreateDTO">лекарство</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные</exception>        
        public Task CreateMedicationAsync(MedicationCreateDTO medicationCreateDTO);

        /// <summary>
        /// Обновление данных о лекарстве
        /// </summary>
        /// <param name="medicationUpdateDTO">данные о лекарстве</param>
        /// <exception cref="IncorrectOrEmptyResultException">Лекарство не зарегистрировано</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные</exception>        
        public Task UpdateMedicationAsync(MedicationUpdateDTO medicationUpdateDTO);

        /// <summary>
        /// Удалить лекарство
        /// </summary>
        /// <param name="medicationId">ИД лекарства</param>
        /// <exception cref="IncorrectOrEmptyResultException">Лекарство не зарегистрировано</exception>        
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные</exception>
        public Task DeleteMedicationAsync(int medicationId);

        /// <summary>
        /// Получить информацию о лекарстве
        /// </summary>
        /// <param name="medicationId">ИД лекарства</param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанное лекарство не существует</exception> 
        public Task<MedicationDTO> GetMedicationByIdAsync(int medicationId);

        /// <summary>
        ///Получить список лекарств
        /// </summary>        
        /// <returns></returns>
        public Task<IEnumerable<MedicationDTO>> GetAllMedicationAsync();
    }
}
