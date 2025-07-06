namespace MetricService.BLL.DTO.DosageForm
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Форма выпуска препарата"
    /// </summary>
    public class DosageFormUpdateDTO: DosageFormBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; set; }       
    }
}
