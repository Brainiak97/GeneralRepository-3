using StateService.Api.Contracts.Dtos.Enums;

namespace StateService.Api.Contracts.Dtos
{
    /// <summary>
    /// Объект данных о приеме лекарств пользователем
    /// </summary>
    public record IntakeDto
    {
        /// <summary>
        /// Идентификатор данных о приеме лекарств пользователем
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Идентификатор данных схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; init; }

        /// <summary>
        /// Дата и время приема
        /// </summary>        
        public DateTime TakenAt { get; init; }

        /// <summary>
        /// Статусы приема (например, "принято", "пропущено", "перенесено")
        /// </summary>       
        public IntakeStatus IntakeStatus { get; init; }

        /// <summary>
        /// Дополнительные заметки (например, причины пропуска)
        /// </summary>        
        public string? Comment { get; init; }
    }
}
