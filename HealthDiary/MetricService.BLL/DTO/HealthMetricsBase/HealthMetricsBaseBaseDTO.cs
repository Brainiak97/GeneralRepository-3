namespace MetricService.BLL.DTO.HealthMetricsBase
{
    /// <summary>
    /// Базовый объект данных базовых медицинских показателей пользователя
    /// </summary>
    public abstract class HealthMetricsBaseBaseDTO
    {     

        /// <summary>
        /// Дата замера показателя
        /// </summary>        
        public DateTime MetricDate { get; set; }

        /// <summary>
        /// Частота сердечных сокращений (ударов/мин)
        /// </summary>        
        public short HeartRate { get; set; }

        /// <summary>
        /// Верхнее артериальное давление (мм рт. ст.)
        /// </summary>
        public short? BloodPressureSys { get; set; }

        /// <summary>
        /// Нижнее артериальное давление (мм рт. ст.)
        /// </summary>
        public short? BloodPressureDia { get; set; }

        /// <summary>
        /// Процент жира в организме
        /// </summary>
        public float? BodyFatPercentage { get; set; }

        /// <summary>
        /// Потребление воды (мл)
        /// </summary>
        public short? WaterIntake { get; set; }
    }
}
