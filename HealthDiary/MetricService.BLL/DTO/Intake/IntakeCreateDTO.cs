namespace MetricService.BLL.DTO.Intake
{
    /// <summary>
    /// Объект для регистрации данных о приеме лекарств пользователем
    /// </summary>
    public class IntakeCreateDTO: IntakeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }       
    }
}
