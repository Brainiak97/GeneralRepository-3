namespace MetricService.BLL.DTO.Intake
{
    public class IntakeDTO: IntakeBaseDTO
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Схема приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }

        
    }
}
