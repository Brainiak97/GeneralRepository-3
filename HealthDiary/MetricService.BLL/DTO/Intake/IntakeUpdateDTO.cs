namespace MetricService.BLL.DTO.Intake
{
    /// <summary>
    /// Объект для изменения данных о приеме лекарств пользователем
    /// </summary>
    public class IntakeUpdateDTO: IntakeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о приеме лекарств пользователем
        /// </summary>
        public int Id { get; set; } 
    }
}
