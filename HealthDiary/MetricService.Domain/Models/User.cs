using Microsoft.EntityFrameworkCore;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Профиль пользователя
    /// </summary>
    /// <seealso cref="BaseModel" />
    [Comment("Пользователь")]
    public class User : BaseModel
    {
        /// <summary>
        /// Рост в сантиметрах
        /// </summary>
        [Comment("рост в сантиметрах")]
        public short Height { get; set; }

        /// <summary>
        /// Вес в килограммах
        /// </summary>
        [Comment("Вес в килограммах")]
        public float Weight { get; set; }
    }
}
