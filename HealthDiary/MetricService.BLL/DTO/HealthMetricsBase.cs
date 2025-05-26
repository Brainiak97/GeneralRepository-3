using MetricService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.BLL.Dto
{
    public  class HealthMetricsBaseDTO
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
