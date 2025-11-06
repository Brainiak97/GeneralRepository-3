namespace PolyclinicService.BLL.Data.Requests
{
    /// <summary>
    /// Запрос для резервации слота приема.
    /// </summary>
    public class UserSlotReservationRequest
    {
        /// <summary>
        /// Идентификатор пользователя, который записывается на прием.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор слота приема.
        /// </summary>
        public int SlotId { get; set; }

        /// <summary>
        /// Указывает предоставлять ли врачу доступ к показателям пользователя.
        /// </summary>
        public bool IssuePermitOfMetrics { get; set; } = false;
    }
}
