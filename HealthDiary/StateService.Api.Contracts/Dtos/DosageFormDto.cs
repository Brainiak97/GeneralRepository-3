namespace StateService.Api.Contracts.Dtos
{
    /// <summary>
    /// Форма выпуска препарата
    /// </summary>    
    public class DosageFormDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>          
        public string Name { get; set; } = string.Empty;
    }
}