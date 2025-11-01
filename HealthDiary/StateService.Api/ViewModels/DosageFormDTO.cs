namespace StateService.Api.ViewModels
{
    /// <summary>
    /// Объект данных в справочнике "Форма выпуска препарата"
    /// </summary>
    public record DosageFormDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Наименование формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>    
        public required string Name { get; init; }
    }
}

