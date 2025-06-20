﻿namespace MetricService.BLL.DTO.PhysicalActivity
{
    public class PhysicalActivityCreateDTO
    {        

        /// <summary>
        /// Наименование физической активности
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Метаболический эквивалент
        /// </summary>
        public float EnergyEquivalent { get; set; }
    }
}
