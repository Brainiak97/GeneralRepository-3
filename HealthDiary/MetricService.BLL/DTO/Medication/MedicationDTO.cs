namespace MetricService.BLL.DTO.MedicationDTO
{
    public class MedicationDTO : MedicationBaseDTO
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }       

        /// <summary>
        /// форма выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>        
        public int DosageFormId { get; set; }       
    }
}
