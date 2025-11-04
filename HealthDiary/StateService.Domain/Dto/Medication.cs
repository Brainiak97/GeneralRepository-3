namespace StateService.Domain.Dto
{
    /// <summary>
    /// Медикаменты
    /// </summary>    
    public class Medication
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование препарата
        /// </summary>         
        public required string Name { get; set; }

        /// <summary>
        /// Идентификатор формы выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>         
        public int DosageFormId { get; set; }

        /// <summary>
        /// Форма выпуска (таблетка, капсул, раствор и т.д.)
        /// </summary>         
        public DosageForm DosageForm { get; set; } = null!;

        /// <summary>
        /// Инструкции по применению
        /// </summary>         
        public required string Instruction { get; set; }
    }
}