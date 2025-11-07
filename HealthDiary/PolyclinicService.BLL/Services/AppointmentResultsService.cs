using AutoMapper;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;
using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models.Entities;
using ReportService.Api.Contracts.Enums;
using ReportService.Api.Contracts.Events;
using Shared.Common.Exceptions;
using Shared.Common.MessageBrokers.RabbitMQ.Publishers;
using UserService.Api.Contracts;

namespace PolyclinicService.BLL.Services;

/// <inheritdoc />
internal class AppointmentResultsService(
    IAppointmentResultsRepository appointmentResultsRepository,
    IServiceModelValidator serviceModelValidator,
    IMapper mapper,
    IUserServiceClient userServiceClient,
    IPolyclinicSchedulesService polyclinicSchedulesService,
    IMessagePublisher messagePublisher)
    : IAppointmentResultsService
{
    /// <inheritdoc />
    public async Task<int> SaveAppointmentResultAsync(SaveAppointmentResultRequest request)
    {
        await serviceModelValidator.ValidateAndThrowAsync(request);

        var slotInfo = await polyclinicSchedulesService.GetAppointmentSlotByIdAsync(request.AppointmentSlotId);
        if (slotInfo is null)
        {
            throw new InvalidOperationException(
                $"Не найдена информация по слоту приёма с идентификатором {request.AppointmentSlotId}");
        }

        if (slotInfo.UserId is null)
        {
            throw new InvalidOperationException(
                $"По слоту {request.AppointmentSlotId} нет записанного пациента");
        }

        var patientInfo = await userServiceClient.GetUserInfoAsync(slotInfo.UserId.Value, CancellationToken.None);
        if (patientInfo is null)
        {
            throw new InvalidOperationException(
                $"Не найдены данные пациента по приёму с идентификатором {request.AppointmentSlotId}");
        }

        var appResult = mapper.Map<AppointmentResult>(request);
        var appResultId = await appointmentResultsRepository.AddAsync(appResult);

        await messagePublisher.PublishAsync(
            new GenerateReportRequested
            {
                EntityId = request.AppointmentSlotId,
                ReportContent = request.ReportContent,
                ReportFormat = ReportFormat.Pdf,
                ReportTemplateId = request.ReportTemplateId,
                NeedSendToEmail = request.NeedSendToEmail,
                EmailAddress = patientInfo.Email,
            });

        return appResultId;
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