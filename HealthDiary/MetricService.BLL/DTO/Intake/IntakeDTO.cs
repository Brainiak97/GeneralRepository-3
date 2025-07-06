namespace MetricService.BLL.DTO.Intake
{
    /// <summary>
    /// Объект данных о приеме лекарств пользователем
    /// </summary>
    public class IntakeDTO: IntakeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о приеме лекарств пользователем
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор данных схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }

        
    }
}
