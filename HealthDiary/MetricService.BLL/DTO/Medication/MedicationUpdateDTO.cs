namespace MetricService.BLL.DTO.MedicationDTO
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Медикаменты"
    /// </summary>
    public class MedicationUpdateDTO : MedicationBaseDTO
    {
        /// <summary>
        /// Идентификатор данны в справочнике
        /// </summary>
        public int Id { get; set; } 
    }
}
