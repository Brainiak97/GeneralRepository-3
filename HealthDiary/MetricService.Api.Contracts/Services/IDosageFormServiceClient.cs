using MetricService.Api.Contracts.Dtos.DosageForm;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с данными справочника "Формы выпуска препарата"
    /// </summary>   
    public interface IDosageFormServiceClient
    {
        const string Controller = "/DosageForm";
        /// <summary>
        /// Зарегистрировать новую форму препарата в справочнике "Формы выпуска препарата"
        /// </summary>
        /// <param name="createDTO">Данные для регистрации формы препарата</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateDosageForm)}")]
        Task CreateDosageForm(DosageFormCreateDTO createDTO);

        /// <summary>
        /// Изменить данные о форме препарата в справочнике "Формы выпуска препарата"
        /// </summary>
        /// <param name="updateDTO">Данные для изменения формы препарата</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateDosageForm)}")]
        Task UpdateDosageForm(DosageFormUpdateDTO updateDTO);

        /// <summary>
        /// Удалить данные формы препарата из справочника "Формы выпуска препарата"
        /// </summary>
        /// <param name="dosageFormId">Идентификатор формы препарата</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteDosageForm)}")]
        Task DeleteDosageForm(int dosageFormId);

        /// <summary>
        /// Получить список форм препаратов из справочника "Формы выпуска препарата"
        /// </summary>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllDosageForms)}")]
        Task<IEnumerable<DosageFormDTO>> GetAllDosageForms();

        /// <summary>
        /// Получить форму препарата из справочника "Формы выпуска препарата"
        /// </summary>
        /// <param name="dosageFormId">Идентификатор формы препарата</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetDosageFormById)}")]
        Task<DosageFormDTO> GetDosageFormById(int dosageFormId);
    }
}
