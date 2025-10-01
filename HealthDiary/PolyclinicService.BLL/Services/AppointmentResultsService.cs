using AutoMapper;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;
using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models.Entities;
using Shared.Common.Exceptions;

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
        await serviceModelValidator.ValidateAndThrowAsync(request);

        var appResult = mapper.Map<AppointmentResult>(request);
        return await appointmentResultsRepository.AddAsync(appResult);
    }

    /// <inheritdoc />
    public async Task UpdateAppointmentResultAsync(UpdateAppointmentResultRequest request)
    {
        await serviceModelValidator.ValidateAndThrowAsync(request);

        var appResult = await appointmentResultsRepository.GetByIdAsync(request.Id);
        if (appResult is null)
        {
            throw new EntryNotFoundException($"Результат приёма не найден по идентификатору {request.Id}");
        }

        appResult.ReportTemplateId = request.ReportTemplateId ?? appResult.ReportTemplateId;
        appResult.ReportContent = request.ReportContent ?? appResult.ReportContent;

        await appointmentResultsRepository.UpdateAsync(appResult);
    }

    /// <inheritdoc />
    public async Task DeleteAppointmentResultAsync(int id) =>
        await appointmentResultsRepository.DeleteAsync(id);

    /// <inheritdoc />
    public async Task<AppointmentResultDto?> GetAppointmentResultByIdAsync(int id) =>
        mapper.Map<AppointmentResultDto?>(await appointmentResultsRepository.GetByIdAsync(id));
}