namespace StateService.Domain.Models
{
    public class UserHealthReport
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Текущий вес пользователя в килограммах.
        /// </summary>
        public double CurrentWeight { get; set; }

        /// <summary>
        /// Целевой (желаемый) вес пользователя в килограммах.
        /// Может использоваться для отслеживания прогресса в похудении или наборе массы.
        /// </summary>
        public double TargetWeight { get; set; }

        /// <summary>
        /// Индекс массы тела (Body Mass Index), вычисляемый на основе текущего веса и роста (CurrentWeight и Height).
        /// Формула: BMI = вес (кг) / (рост (м))²
        /// Рост делится на 100, чтобы перевести из см в м.
        /// </summary>
        public double BMI => CurrentWeight / (Height / 100.0 * Height / 100.0); 

        /// <summary>
        /// Рост пользователя в сантиметрах.
        /// Используется для расчёта индекса массы тела (BMI).
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Количество потреблённых калорий за день.
        /// Берётся из сервиса питания (например, пользователь добавлял блюда в дневник).
        /// </summary>
        public int CaloriesConsumed { get; set; }

        /// <summary>
        /// Количество сожжённых калорий за день.
        /// Определяется на основе активности пользователя (тренировки, шаги и т.д.).
        /// </summary>
        public int CaloriesBurned { get; set; }

        /// <summary>
        /// Оставшееся количество калорий, которые можно употребить за день без превышения нормы.
        /// Вычисляется как разница между TDEE и фактически потреблёнными калориями.
        /// Если результат отрицательный — возвращает 0.
        /// </summary>
        public int CaloriesLeftToday => Math.Max(0, TDEE - CaloriesConsumed);

        /// <summary>
        /// Общий расход энергии в день (Total Daily Energy Expenditure).
        /// Это оценка того, сколько калорий пользователь тратит в день с учётом активности.
        /// Зависит от веса, возраста, пола, роста и уровня активности.
        /// </summary>
        public int TDEE { get; set; }

        /// <summary>
        /// Количество шагов, сделанных пользователем за день.
        /// Может быть взято из фитнес-трекера, телефона или введено вручную.
        /// </summary>
        public int Steps { get; set; }

        /// <summary>
        /// Продолжительность сна в минутах за последнюю ночь.
        /// Может использоваться для анализа качества и длительности сна.
        /// </summary>
        public int SleepMinutes { get; set; }

        /// <summary>
        /// Список рекомендаций, основанных на анализе данных о здоровье пользователя.
        /// Например: "Вы не набрали нужное количество шагов", "Вы превысили норму калорий" и т.д.
        /// </summary>
        public List<string>? Recommendations { get; set; } = [];
    }
}
