namespace MetricService.BLL.DTO.Regimen
{
    /// <summary>
    /// Объект для изменения данных схема прима медикаментов пользователем
    /// </summary>
    public class RegimenUpdateDTO: RegimenBaseDTO
    {
        /// <summary>
        /// Идентификатор данных схемы приема медикаментов пользователем
        /// </summary>
        public int Id { get; set; }         
    }
}
