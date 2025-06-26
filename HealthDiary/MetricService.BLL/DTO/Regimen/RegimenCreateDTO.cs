namespace MetricService.BLL.DTO.Regimen
{
    public class RegimenCreateDTO: RegimenBaseDTO
    {
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
