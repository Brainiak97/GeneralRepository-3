namespace MetricService.BLL.DTO.MedicationDTO
{
    public class MedicationCreateDTO : MedicationBaseDTO
    {   
        /// <summary>
        /// форма выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>        
        public int DosageFormId { get; set; }        
    }
}
