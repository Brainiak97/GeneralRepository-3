﻿namespace MetricService.Api.Contracts.Dtos.Workout
{
    /// <summary>
    /// Объект базовых данных о тренировке пользователя
    /// </summary>
    public abstract record WorkoutBaseDTO
    {
        /// <summary>
        /// Идентификатор данных и справочника "Физическая активность"
        /// </summary>
        public int PhysicalActivityId { get; init; }

        /// <summary>
        /// Время начала тренировки
        /// </summary>        
        public DateTime StartTime { get; init; }

        /// <summary>
        /// Время окончания тренировки
        /// </summary>       
        public DateTime EndTime { get; init; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Потраченные калории за тренировку
        /// </summary>
        public float CaloriesBurned { get; init; }
    }
}
