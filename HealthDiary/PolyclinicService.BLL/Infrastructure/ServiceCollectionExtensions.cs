using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PolyclinicService.BLL.Calculators;
using PolyclinicService.BLL.Common.ServiceModelsValidator;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;
using PolyclinicService.BLL.Data.Commands;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;
using PolyclinicService.BLL.Services;
using PolyclinicService.BLL.Validators;

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
            .AddCalculators();
    
    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<IPolyclinicsService, Services.PolyclinicService>()
            .AddScoped<IPolyclinicSchedulesService, PolyclinicSchedulesService>()
            .AddScoped<IDoctorsService, DoctorsService>()
            .AddScoped<IAppointmentResultsService, AppointmentResultsService>();

    private static IServiceCollection AddValidators(this IServiceCollection services) =>
        services
            .AddSingleton<IValidatorsProvider, ValidatorsProvider>()
            .AddSingleton<IServiceModelValidator, ServiceModelValidator>()
            .AddSingleton<IValidator<UserSlotReservationRequest>, UserSlotReservationRequestValidator>()
            .AddSingleton<IValidator<AddPolyclinicRequest>, AddPolyclinicRequestValidator>()
            .AddSingleton<IValidator<UpdatePolyclinicRequest>, UpdatePolyclinicRequestValidator>()
            .AddSingleton<IValidator<AddDoctorRequest>, AddDoctorRequestValidator>()
            .AddSingleton<IValidator<UpdateDoctorRequest>, UpdateDoctorRequestValidator>()
            .AddSingleton<IValidator<AddAppoinmentSlotCommand>, AddAppointmentSlotCommandValidator>()
            .AddSingleton<IValidator<AddAppointmentSlotsByTemplateCommand>, AddAppointmentSlotsByTemplateCommandValidator>()
            .AddSingleton<IValidator<UpdateAppointmentSlotCommand>, UpdateAppointmentSlotCommandValidator>()
            .AddSingleton<IValidator<UpdateAppointmentSlotStatusCommand>, UpdateAppointmentSlotStatusCommandValidator>()
            .AddSingleton<IValidator<DoctorActiveAppointmentSlotsCommand>, DoctorActiveAppointmentSlotsCommandValidator>()
            .AddSingleton<IValidator<PolyclinicAppointmentSlotsByDateCommand>, PolyclinicAppointmentSlotsByDateCommandValidator>()
            .AddSingleton<IValidator<DeletePolyclinicAppointmentSlotsByFilterCommand>, DeletePolyclinicAppointmentSlotsByFilterCommandValidator>()
            .AddSingleton<IValidator<SaveAppointmentResultRequest>, SaveAppointmentResultRequestValidator>()
            .AddSingleton<IValidator<UpdateAppointmentResultRequest>, UpdateAppointmentResultRequestValidator>()
            .AddSingleton<IValidator<GetPatientAppointmentSlotsCommand>, GetPatientAppointmentSlotsCommandValidator>();

    private static IServiceCollection AddCalculators(this IServiceCollection services) =>
        services.AddSingleton<IAppointmentSlotsCalculator, AppointmentSlotsCalculator>();
}