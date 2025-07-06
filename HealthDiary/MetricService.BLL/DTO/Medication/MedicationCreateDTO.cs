namespace MetricService.BLL.DTO.MedicationDTO
{
    /// <summary>
    /// Объект для регистрации данных в справочнике "Медикаменты"
    /// </summary>
    public class MedicationCreateDTO : MedicationBaseDTO
    {
        /// <summary>
        /// Идентификатор формы выпуска (таблетка, капсул, раствор и т.д.) из справочника "Форма выпуска препарата"
        /// </summary>        
        public int DosageFormId { get; set; }        
    }
}
