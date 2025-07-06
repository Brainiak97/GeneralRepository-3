namespace MetricService.Domain.Models
{
    /// <summary>
    /// Медикаменты
    /// </summary>
    public class Medication: BaseModel
    {
        /// <summary>
        /// Наименование препарата
        /// </summary>        
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>        
        public int DosageFormId { get; set; }

        /// <summary>
        /// Форма выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>        
        public DosageForm DosageForm { get; set; } = null!;

        /// <summary>
        /// Инструкции по применению
        /// </summary>        
        public string Instruction { get; set; } = string.Empty;
    }
}
