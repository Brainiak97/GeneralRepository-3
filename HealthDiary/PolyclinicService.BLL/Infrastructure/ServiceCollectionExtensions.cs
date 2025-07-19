using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PolyclinicService.BLL.Calculators;
using PolyclinicService.BLL.Common.ServiceModelsValidator;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;
using PolyclinicService.BLL.Mappers;
using PolyclinicService.BLL.Services;
using PolyclinicService.BLL.Validators;
using IValidatorFactory = PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces.IValidatorFactory;

namespace PolyclinicService.BLL.Infrastructure;

/// <summary>
/// Расширения для регистрации сервисов слоя бизнес-логики.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Зарегистрировать сервисы слоя бизнес-логики.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services
            .AddServices()
            .AddValidators()
            .AddCalculators()
            .AddMappers();
    
    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<IPolyclinicsService, Services.PolyclinicService>()
            .AddScoped<IPolyclinicSchedulesService, PolyclinicSchedulesService>()
            .AddScoped<IDoctorsService, DoctorsService>()
            .AddScoped<IAppointmentsService, AppointmentsService>();

    private static IServiceCollection AddValidators(this IServiceCollection services) =>
        services
            .AddSingleton<IValidatorFactory, ValidationFactory>()
            .AddSingleton<IServiceModelValidator, ServiceModelValidator>()
            .AddSingleton<IValidator<AddPolyclinicRequest>, AddPolyclinicRequestValidator>()
            .AddSingleton<IValidator<UpdatePolyclinicRequest>, UpdatePolyclinicRequestValidator>()
            .AddSingleton<IValidator<AddDoctorRequest>, AddDoctorRequestValidator>()
            .AddSingleton<IValidator<UpdateDoctorRequest>, UpdateDoctorRequestValidator>()
            .AddSingleton<IValidator<AddAppoinmentSlotRequest>, AddAppointmentSlotRequestValidator>()
            .AddSingleton<IValidator<AddAppointmentSlotsByTemplateRequest>, AddAppointmentSlotsByTemplateRequestValidator>()
            .AddSingleton<IValidator<UpdateAppointmentSlotRequest>, UpdateAppointmentSlotRequestValidator>()
            .AddSingleton<IValidator<UpdateAppointmentSlotStatusRequest>, UpdateAppointmentSlotStatusRequestValidator>()
            .AddSingleton<IValidator<DoctorActiveAppointmentSlotsRequest>, DoctorActiveAppointmentSlotsRequestValidator>()
            .AddSingleton<IValidator<PolyclinicAppointmentSlotsByDateRequest>, PolyclinicAppointmentSlotsByDateRequestValidator>()
            .AddSingleton<IValidator<DeletePolyclinicAppointmentSlotsByFilterRequest>, DeletePolyclinicAppointmentSlotsByFilterRequestValidator>();

    private static IServiceCollection AddCalculators(this IServiceCollection services) =>
        services.AddSingleton<IAppointmentSlotsCalculator, AppointmentSlotsCalculator>();

    private static IServiceCollection AddMappers(this IServiceCollection services) =>
        services.AddAutoMapper(cfg => cfg.AddProfile<PolyclinicServiceMapperProfile>());
}