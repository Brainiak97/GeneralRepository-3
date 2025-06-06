using MetricService.BLL.DTO.HealthMetricsBase;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class HealthMetricsBaseMapper
    {
        public static HealthMetricsBaseCreateDTO ToHealthMetricsBaseCreateDTO(this HealthMetricsBase healthMetricsBase)
        {
            return new HealthMetricsBaseCreateDTO
            {
                BloodPressureDia = healthMetricsBase.BloodPressureDia,
                BloodPressureSys = healthMetricsBase.BloodPressureSys,
                BodyFatPercentage = healthMetricsBase.BodyFatPercentage,
                HeartRate = healthMetricsBase.HeartRate,
                MetricDate = healthMetricsBase.MetricDate,
                UserId = healthMetricsBase.UserId,
                WaterIntake = healthMetricsBase.WaterIntake
            };
        }

        public static HealthMetricsBase ToHealthMetricsBase(this HealthMetricsBaseCreateDTO healthMetricsBaseCreateDTO)
        {
            return new HealthMetricsBase
            {
                BloodPressureSys = healthMetricsBaseCreateDTO.BloodPressureSys,
                BloodPressureDia = healthMetricsBaseCreateDTO.BloodPressureDia,
                BodyFatPercentage = healthMetricsBaseCreateDTO.BodyFatPercentage,
                HeartRate = healthMetricsBaseCreateDTO.HeartRate,
                Id = 0,
                MetricDate = healthMetricsBaseCreateDTO.MetricDate,
                UserId = healthMetricsBaseCreateDTO.UserId,
                WaterIntake = healthMetricsBaseCreateDTO.WaterIntake
            };
        }

        public static HealthMetricsBaseUpdateDTO ToHealthMetricsBaseUpdateDTO(this HealthMetricsBase healthMetricsBase)
        {
            return new HealthMetricsBaseUpdateDTO
            {
                BloodPressureDia = healthMetricsBase.BloodPressureDia,
                WaterIntake = healthMetricsBase.WaterIntake,
                MetricDate = healthMetricsBase.MetricDate,
                Id = healthMetricsBase.Id,
                HeartRate = healthMetricsBase.HeartRate,
                BloodPressureSys = healthMetricsBase.BloodPressureSys,
                BodyFatPercentage = healthMetricsBase.BodyFatPercentage,
            };
        }

        public static HealthMetricsBase ToHealthMetricsBase(this HealthMetricsBaseUpdateDTO healthMetricsBaseUpdateDTO, int userId)
        {
            return new HealthMetricsBase
            {
                BloodPressureDia = healthMetricsBaseUpdateDTO.BloodPressureDia,
                BloodPressureSys = healthMetricsBaseUpdateDTO.BloodPressureSys,
                BodyFatPercentage = healthMetricsBaseUpdateDTO.BodyFatPercentage,
                HeartRate = healthMetricsBaseUpdateDTO.HeartRate,
                Id = healthMetricsBaseUpdateDTO.Id,
                MetricDate = healthMetricsBaseUpdateDTO.MetricDate,
                WaterIntake = healthMetricsBaseUpdateDTO.WaterIntake,
                UserId = userId
            };
        }

        public static HealthMetricsBaseDTO ToHealthMetricsBaseDTO(this HealthMetricsBase healthMetricsBase)
        {
            return new HealthMetricsBaseDTO
            {
                UserId = healthMetricsBase.UserId,
                WaterIntake = healthMetricsBase.WaterIntake,
                MetricDate = healthMetricsBase.MetricDate,
                Id = healthMetricsBase.Id,
                HeartRate = healthMetricsBase.HeartRate,
                BodyFatPercentage = healthMetricsBase.BodyFatPercentage,
                BloodPressureDia = healthMetricsBase.BloodPressureDia,
                BloodPressureSys = healthMetricsBase.BloodPressureSys
            };
        }

        public static IEnumerable<HealthMetricsBaseDTO> ToHealthMetricsBaseDTO(this IEnumerable<HealthMetricsBase> healthMetricsBase)
        {
            var result = new List<HealthMetricsBaseDTO>();
            foreach (var healthMetric in healthMetricsBase)
            {
                result.Add(ToHealthMetricsBaseDTO(healthMetric));
            }
            return result;
        }
    }
}
