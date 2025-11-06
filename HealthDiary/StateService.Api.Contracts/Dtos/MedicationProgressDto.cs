namespace StateService.Api.Contracts.Dtos
{
    /// <summary>
    /// Развернутая информация по приему медикаментов по схеме
    /// </summary>    
    public record MedicationProgressDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; init; }

        /// <summary>
        /// развернутая схема приема лекарств
        /// </summary>        
        public required List<RegimenProgressDto> Regimens { get; init; }
    }
}
