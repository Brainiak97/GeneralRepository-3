namespace StateService.Api.Contracts.Dtos
{
    public class UserHealthReportDto
    {
        /// <summary>
        /// Дата отчёта
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Медицинские показатели (несколько измерений за день)
        /// </summary>
        public List<HealthMetricsDto> HealthMetrics { get; set; } = [];

        /// <summary>
        /// Тренировки за день
        /// </summary>
        public List<WorkoutDto> PhysicalActivity { get; set; } = [];

        /// <summary>
        /// Данные о сне (одна или несколько сессий)
        /// </summary>
        public List<SleepDto> Sleep { get; set; } = [];

        /// <summary>
        /// Приёмы пищи за день
        /// </summary>
        public List<object> FoodData { get; set; } = [];
    }
}
