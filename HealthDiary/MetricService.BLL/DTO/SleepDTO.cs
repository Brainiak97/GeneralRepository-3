using MetricService.Domain.Models;

namespace MetricService.BLL.Dto
{
    public class SleepDTO 
    {
        /// <summary>
        /// идентификатор
        /// 0 - для новых записей
        /// >0 - для существующих записей
        /// </summary>    
        public int Id { get; set; }
        /// <summary>
        /// идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }

        /// <summary>
        /// идентификатор пользователя
        /// </summary>        
        public UserDTO User { get; set; } = null!;

        /// <summary>
        /// время начала сна
        /// </summary>        
        public DateTime StartSleep { get; set; }

        /// <summary>
        /// время окончания сна
        /// </summary>        
        public DateTime EndSleep { get; set; }

        /// <summary>
        /// качество сна по 5-ой системе
        /// </summary>
        public Int16 QualityRating { get; set; }

        /// <summary>
        /// примечания о качестве сна
        /// </summary>
        public string? Comment { get; set; }
    }
}
