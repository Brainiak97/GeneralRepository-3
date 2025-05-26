namespace MetricService.Domain.Models
{
    /// <summary>
    /// базовые медицинские показатели
    /// </summary>
    public class HealthMetricsBase: BaseModel
    {
         /// <summary>
        /// идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }

        /// <summary>
        /// идентификатор пользователя
        /// </summary>        
        public User User { get; set; } = null!; 

        /// <summary>
        /// дата замера показателя
        /// </summary>        
        public DateTime MetricDate { get; set; }

        /// <summary>
        /// частота сердечных сокращений (ударов/мин)
        /// </summary>        
        public Int16 HeartRate { get; set; }

        /// <summary>
        /// Верхнее артериальное давление (мм рт. ст.)
        /// </summary>
        public Int16? BloodPressureSys { get; set; }

        /// <summary>
        /// Нижнее артериальное давление (мм рт. ст.)
        /// </summary>
        public Int16? BloodPressureDia { get; set; }

        /// <summary>
        /// Процент жира в организме
        /// </summary>
        public float? BodyFatPercentage { get; set; }

        /// <summary>
        /// Потребление воды (мл)
        /// </summary>
        public Int16? WaterIntake { get; set; }

    }
}
