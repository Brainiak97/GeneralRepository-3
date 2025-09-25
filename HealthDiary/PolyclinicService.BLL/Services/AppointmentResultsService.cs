using AutoMapper;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;
using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.BLL.Services;

/// <inheritdoc />
internal class AppointmentResultsService(
    IAppointmentResultsRepository appointmentResultsRepository,
    IServiceModelValidator serviceModelValidator,
    IMapper mapper)
    : IAppointmentResultsService
{
    /// <inheritdoc />
    public async Task<int> SaveAppointmentResultAsync(SaveAppointmentResultRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        await serviceModelValidator.ValidateAndThrowAsync(request);

        return await appointmentResultsRepository.AddAsync(mapper.Map<AppointmentResult>(request));
    }

    /// <inheritdoc />
    public async Task UpdateAppointmentResultAsync(UpdateAppointmentResultRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        await serviceModelValidator.ValidateAndThrowAsync(request);

        var appResult = await appointmentResultsRepository.GetByIdAsync(request.Id);
        if (appResult is null)
        {
            throw new InvalidOperationException($"Результат приёма не найден по идентификатору {request.Id}");
        }

        appResult.AppointmentSlotId = request.AppointmentSlotId ?? appResult.AppointmentSlotId;
        appResult.ReportTemplateId = request.ReportTemplateId ?? appResult.ReportTemplateId;
        appResult.ReportContent = request.ReportContent ?? appResult.ReportContent;

        await appointmentResultsRepository.UpdateAsync(mapper.Map<AppointmentResult>(request));
    }

    /// <inheritdoc />
    public async Task DeleteAppointmentResultAsync(int id) =>
        await appointmentResultsRepository.DeleteAsync(id);

    /// <inheritdoc />
    public async Task<AppointmentResultDto?> GetAppointmentResultByIdAsync(int id) =>
        id <= 0
            ? null
            : mapper.Map<AppointmentResultDto?>(await appointmentResultsRepository.GetByIdAsync(id));

    /// <inheritdoc />
    public async Task<AppointmentResultExtDto[]> GetPatientAppointmentResultsWithSlotInfoAsync(int patientId, DateTime? date)
    {
        if (patientId <= 0)
        {
            return [];
        }

        var results = await appointmentResultsRepository.GetPatientAppointmentResultsWithSlotInfoAsync(
            patientId,
            date) ?? [];

        return results.Select(mapper.Map<AppointmentResultExtDto>).ToArray();
    }
}