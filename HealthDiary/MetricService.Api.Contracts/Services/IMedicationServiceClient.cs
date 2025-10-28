using MetricService.Api.Contracts.Dtos.Medication;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы со справочником "Медикаменты"
    /// </summary>    
    public interface IMedicationServiceClient
    {
        const string Controller = "/Medication";
        /// <summary>
        /// Зарегистрировать медикамент в справочнике "Медикаменты"
        /// </summary>
        /// <param name="medicationCreateDTO">Данные для регистрации медикамента в справочнике</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateMedication)}")]
        Task CreateMedication(MedicationCreateDTO medicationCreateDTO);

        /// <summary>
        /// Изменить данные регистрации медикамента в справочнике "Медикаменты"
        /// </summary>
        /// <param name="medicationUpdateDTO">Измененные данные медикамента</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateMedication)}")]

        Task UpdateMedication(MedicationUpdateDTO medicationUpdateDTO);

        /// <summary>
        /// Удалить медикамент из справочника "Медикаменты"
        /// </summary>
        /// <param name="medicationid">Идентификатор данных медикамента</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteMedication)}")]
        Task DeleteMedication(int medicationid);

        /// <summary>
        /// Получить список медикаментов из справочника "Медикаменты"
        /// </summary>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllMedications)}")]
        Task<IEnumerable<MedicationDTO>> GetAllMedications();

        /// <summary>
        /// Получить медикамент из справочника
        /// </summary>
        /// <param name="medicationid">Идентификатор данных медикамента</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetMedicationById)}")]
        Task<MedicationDTO> GetMedicationById(int medicationid);
    }
}
