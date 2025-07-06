namespace MetricService.BLL.DTO.DosageForm
{
    /// <summary>
    /// Объект базовых данных в справочнике "Форма выпуска препарата"
    /// </summary>
    public abstract class DosageFormBaseDTO
    {
        /// <summary>
        /// Наименование формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>    
        public string Name { get; set; } = string.Empty;
    }
}
