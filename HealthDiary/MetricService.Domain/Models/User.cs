namespace MetricService.Domain.Models
{
    /// <summary>
    /// Профиль пользователя
    /// </summary>
    /// <seealso cref="BaseModel" />
    public class User:BaseModel
    {
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Рост в сантиметрах
        /// </summary>
        public short Height { get; set; }

        /// <summary>
        /// Вес в килограммах
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        /// <returns>Значение возраста (лет)</returns>
        public short Age
        {
            get
            {
                return (short)(( DateTime.Now - DateOfBirth).TotalDays / 365.25);
            }
        }
    }
}
