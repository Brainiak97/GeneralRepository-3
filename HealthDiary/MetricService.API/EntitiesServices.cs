using MetricService.BLL.Interfaces;
using MetricService.BLL.Services;
using MetricService.BLL.Validators;
using MetricService.DAL.Interfaces;
using MetricService.DAL.Repositories;
using MetricService.Domain.Models;

namespace MetricService.API
{
    /// <summary>
    /// Пкетная регистрация зависимостей сущностей
    /// </summary>
    public static class EntitiesServices
    {
        /// <summary>
        /// Регистрирует зависимости
        /// </summary>
        /// <param name="builder"></param>
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, BLL.Services.UserService>();
            builder.Services.AddScoped<IValidator<User>, UserValidator>();

            builder.Services.AddScoped<ISleepRepository, SleepRepository>();
            builder.Services.AddScoped<ISleepService, SleepService>();
            builder.Services.AddScoped<IValidator<Sleep>, SleepValidator>();

            builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            builder.Services.AddScoped<IWorkoutService, WorkoutService>();
            builder.Services.AddScoped<IValidator<Workout>, WorkoutValidator>();

            builder.Services.AddScoped<IPhysicalActivityRepository, PhysicalActivityRepository>();
            builder.Services.AddScoped<IPhysicalActivityService, PhysicalActivityService>();
            builder.Services.AddScoped<IValidator<PhysicalActivity>, PhysicalActivityValidator>();

            builder.Services.AddScoped<IHealthMetricRepository, HealthMetricRepository>();
            builder.Services.AddScoped<IHealthMetricService, HealthMetricService>();

            builder.Services.AddScoped<IHealthMetricValueRepository, HealthMetricValueRepository>();
            builder.Services.AddScoped<IHealthMetricValueService, HealthMetricValueService>();

            builder.Services.AddScoped<IAnalysisCategoryRepository, AnalysisCategoryRepository>();
            builder.Services.AddScoped<IAnalysisCategoryService, AnalysisCategoryService>();
            builder.Services.AddScoped<IValidator<AnalysisCategory>, AnalysisCategoryValidator>();

            builder.Services.AddScoped<IAnalysisTypeRepository, AnalysisTypeRepository>();
            builder.Services.AddScoped<IAnalysisTypeService, AnalysisTypeService>();
            builder.Services.AddScoped<IValidator<AnalysisType>, AnalysisTypeValidator>();

            builder.Services.AddScoped<IAnalysisResultRepository, AnalysisResultRepository>();
            builder.Services.AddScoped<IAnalysisResultService, AnalysisResultService>();
            builder.Services.AddScoped<IValidator<AnalysisResult>, AnalysisResultValidator>();

            builder.Services.AddScoped<IDosageFormRepository, DosageFormRepository>();
            builder.Services.AddScoped<IDosageFormService, DosageFormService>();

            builder.Services.AddScoped<IIntakeRepository, IntakeRepository>();
            builder.Services.AddScoped<IIntakeService, IntakeService>();
            builder.Services.AddScoped<IValidator<Intake>, IntakeValidator>();

            builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
            builder.Services.AddScoped<IMedicationService, MedicationService>();

            builder.Services.AddScoped<IRegimenRepository, RegimenRepository>();
            builder.Services.AddScoped<IRegimenService, RegimenService>();
            builder.Services.AddScoped<IValidator<Regimen>, RegimenValidator>();

            builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
            builder.Services.AddScoped<IReminderService, ReminderService>();
            builder.Services.AddScoped<IValidator<Reminder>, ReminderValidator>();

            builder.Services.AddScoped<IAccessToMetricsRepository, AccessToMetricsRepository>();
            builder.Services.AddScoped<IAccessToMetricsService, AccessToMetricsService>();
        }
    }
}
