namespace StateService.Domain.Dto
{
    /// <summary>
    /// Форма выпуска препарата
    /// </summary>    
    public class DosageForm
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