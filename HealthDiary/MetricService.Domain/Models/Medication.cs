namespace MetricService.Domain.Models
{
    public class Medication: BaseModel
    {
        /// <summary>
        /// название препарата
        /// </summary>        
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// форма выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>        
        public int DosageFormId { get; set; }

        /// <summary>
        /// форма выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>        
        public DosageForm DosageForm { get; set; } = null!;

        /// <summary>
        /// Инструкции по применению
        /// </summary>        
        public string Instruction { get; set; } = string.Empty;
    }
}
