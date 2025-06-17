namespace MetricService.BLL.DTO.Intake
{
    public class IntakeCreateDTO: IntakeBaseDTO
    {        
        /// <summary>
        /// Схема приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }       
    }
}
