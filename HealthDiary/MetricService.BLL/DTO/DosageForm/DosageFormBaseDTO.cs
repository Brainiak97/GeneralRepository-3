namespace MetricService.BLL.DTO.DosageForm
{
    public class DosageFormBaseDTO
    {
        /// <summary>
        /// Наименование формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>    
        public string Name { get; set; } = string.Empty;
    }
}
