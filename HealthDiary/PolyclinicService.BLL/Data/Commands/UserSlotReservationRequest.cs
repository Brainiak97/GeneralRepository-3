namespace PolyclinicService.BLL.Data.Commands;

/// <summary>
/// Команда для резервации слота приема.
/// </summary>
public record UserSlotReservationCommand
{
    /// <summary>
    /// Идентификатор пользователя, который записывается на прием.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// Идентификатор слота приема.
    /// </summary>
    public int SlotId { get; init; }

    /// <summary>
    /// Указывает предоставлять ли врачу доступ к показателям пользователя.
    /// </summary>
    public bool IssuePermitOfMetrics { get; init; }
}