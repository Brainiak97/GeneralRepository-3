using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
        /// Дата рождения
        /// </summary>
        [Comment("дата рождения")]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

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

        /// <summary>
        /// Возраст
        /// </summary>
        /// <returns>Значение возраста (лет)</returns>
        public short Age
        {
            get
            {
                return (short)((DateTime.Now - DateOfBirth).TotalDays / 365.25);
            }
        }
    }
}
