namespace StateService.Domain.Dto
{
    /// <summary>
    /// Развернутая информация по приему медикаментов по схеме
    /// </summary>    
    public class MedicationProgress
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>    
        public int UserId { get; set; }

        /// <summary>
        /// развернутая схема приема лекарств
        /// </summary>    
        public required IEnumerable<RegimenProgress> Regimens { get; set; }
    }
}
