namespace MetricService.BLL.DTO.Regimen
{
    public class RegimenDTO: RegimenBaseDTO
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>       
        public int UserId { get; set; }        

        /// <summary>
        /// медицинский препарат
        /// </summary>       
        public int MedicationId { get; set; }        
    }
}
