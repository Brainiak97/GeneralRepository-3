namespace MetricService.BLL.DTO.Regimen
{
    /// <summary>
    /// Объект данных схемы приема медикаментов пользователем
    /// </summary>
    public class RegimenDTO: RegimenBaseDTO
    {
        /// <summary>
        /// Идентификатор данных схемы приема медикаментов пользователем
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>       
        public int UserId { get; set; }        

        /// <summary>
        /// Идентификатор данных из справочника "Медикаменты"
        /// </summary>       
        public int MedicationId { get; set; }        
    }
}
