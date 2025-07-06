namespace MetricService.Domain.Models
{
    /// <summary>
    /// Форма выпуска препарата
    /// </summary>
    public class DosageForm: BaseModel
    {
        /// <summary>
        /// Наименование формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>        
        public string Name { get; set; } = string.Empty;
    }
}
