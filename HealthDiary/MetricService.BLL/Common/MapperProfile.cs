using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AccessToMetrics;
using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.DTO.DosageForm;
using MetricService.BLL.DTO.HealthCondition;
using MetricService.BLL.DTO.HealthMetric;
using MetricService.BLL.DTO.Intake;
using MetricService.BLL.DTO.MedicationDTO;
using MetricService.BLL.DTO.PhysicalActivity;
using MetricService.BLL.DTO.Regimen;
using MetricService.BLL.DTO.Reminder;
using MetricService.BLL.DTO.Sleep;
using MetricService.BLL.DTO.Workout;
using MetricService.Domain.Models;

namespace MetricService.BLL.Common
{
    /// <summary>
    /// Профиль автомаппера
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Создает объект профиля автомаппера
        /// </summary>
        public MapperProfile()
        {
            CreateMap<Workout, WorkoutCreateDTO>().ReverseMap();
            CreateMap<Workout, WorkoutUpdateDTO>().ReverseMap();
            CreateMap<Workout, WorkoutDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Sleep, SleepCreateDTO>().ReverseMap();
            CreateMap<Sleep, SleepUpdateDTO>().ReverseMap();
            CreateMap<Sleep, SleepDTO>().ReverseMap();

            CreateMap<PhysicalActivity, PhysicalActivityCreateDTO>().ReverseMap();
            CreateMap<PhysicalActivity, PhysicalActivityUpdateDTO>().ReverseMap();
            CreateMap<PhysicalActivity, PhysicalActivityDTO>().ReverseMap();

            CreateMap<HealthCondition, HealthConditionCreateDTO>().ReverseMap();
            CreateMap<HealthCondition, HealthConditionUpdateDTO>().ReverseMap();

            CreateMap<HealthMetric, HealthMetricCreateDTO>().ReverseMap();
            CreateMap<HealthMetric, HealthMetricUpdateDTO>().ReverseMap();
            CreateMap<HealthMetric, HealthMetricDTO>().ReverseMap();

            CreateMap<HealthMetricValue, HealthMetricValueCreateDTO>().ReverseMap();
            CreateMap<HealthMetricValue, HealthMetricValueUpdateDTO>().ReverseMap();
            CreateMap<HealthMetricValue, HealthMetricValueDTO>().ReverseMap();

            CreateMap<AnalysisCategory, AnalysisCategoryCreateDTO>().ReverseMap();
            CreateMap<AnalysisCategory, AnalysisCategoryUpdateDTO>().ReverseMap();
            CreateMap<AnalysisCategory, AnalysisCategoryDTO>().ReverseMap();

            CreateMap<AnalysisType, AnalysisTypeCreateDTO>().ReverseMap();
            CreateMap<AnalysisType, AnalysisTypeUpdateDTO>().ReverseMap();
            CreateMap<AnalysisType, AnalysisTypeDTO>().ReverseMap();

            CreateMap<AnalysisResult, AnalysisResultCreateDTO>().ReverseMap();
            CreateMap<AnalysisResult, AnalysisResultUpdateDTO>().ReverseMap();
            CreateMap<AnalysisResult, AnalysisResultDTO>().ReverseMap();

            CreateMap<DosageForm, DosageFormDTO>().ReverseMap();
            CreateMap<DosageForm, DosageFormCreateDTO>().ReverseMap();
            CreateMap<DosageForm, DosageFormUpdateDTO>().ReverseMap();

            CreateMap<Intake, IntakeDTO>().ReverseMap();
            CreateMap<Intake, IntakeCreateDTO>().ReverseMap();
            CreateMap<Intake, IntakeUpdateDTO>().ReverseMap();

            CreateMap<Medication, MedicationDTO>().ReverseMap();
            CreateMap<Medication, MedicationCreateDTO>().ReverseMap();
            CreateMap<Medication, MedicationUpdateDTO>().ReverseMap();

            CreateMap<Regimen, RegimenDTO>().ReverseMap();
            CreateMap<Regimen, RegimenCreateDTO>().ReverseMap();
            CreateMap<Regimen, RegimenUpdateDTO>().ReverseMap();

            CreateMap<Reminder, ReminderDTO>().ReverseMap();
            CreateMap<Reminder, ReminderCreateDTO>().ReverseMap();
            CreateMap<Reminder, ReminderUpdateDTO>().ReverseMap();

            CreateMap<AccessToMetrics, AccessToMetricsDTO>().ReverseMap();
            CreateMap<AccessToMetrics, AccessToMetricsCreateDTO>().ReverseMap();
            CreateMap<AccessToMetrics, AccessToMetricsUpdateDTO>().ReverseMap();
        }
    }
}
