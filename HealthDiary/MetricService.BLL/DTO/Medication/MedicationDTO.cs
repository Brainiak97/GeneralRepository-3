namespace MetricService.BLL.DTO.MedicationDTO
{
    /// <summary>
    /// Объект данных в справочнике "Медикаменты"
    /// </summary>
    public class MedicationDTO : MedicationBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор формы выпуска (таблетка, капсул, раствор и т.д.) из справочника "Форма выпуска препарата"
        /// </summary>        
        public int DosageFormId { get; set; }       
    }
}
