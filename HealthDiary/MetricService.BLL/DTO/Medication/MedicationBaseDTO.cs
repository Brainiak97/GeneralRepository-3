using MetricService.Domain.Models;

namespace MetricService.BLL.DTO.MedicationDTO
{
    /// <summary>
    /// Объект базовых данных для справочника "Медикаменты"
    /// </summary>
    public class MedicationBaseDTO
    {        
        /// <summary>
        /// Наименование препарата
        /// </summary>        
        public string Name { get; set; } = string.Empty;  

        /// <summary>
        /// Инструкции по применению
        /// </summary>        
        public string Instruction { get; set; } = string.Empty;
    }
}
