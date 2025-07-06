namespace MetricService.BLL.DTO.PhysicalActivity
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Физическая активность"
    /// </summary>
    public class PhysicalActivityUpdateDTO : PhysicalActivityBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; set; }
    }
}
