using AutoMapper;
using MetricService.Api.Contracts;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;
using PolyclinicService.BLL.Data;
using PolyclinicService.BLL.Data.Commands;
using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Interfaces;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models;
using PolyclinicService.Domain.Models.Entities;
using Shared.Common.Exceptions;
using MetricService.Api.Contracts.Dtos.AccessToMetrics;

namespace PolyclinicService.BLL.Services;

/// <inheritdoc />
internal class PolyclinicSchedulesService(
    IMetricServiceClient metricServiceClient,
    IAppointmentSlotsRepository appointmentSlotsRepository,
    IAppointmentSlotsCalculator appointmentSlotsCalculator,
    IServiceModelValidator modelValidator,
    IMapper mapper)
    : IPolyclinicSchedulesService
{
    /// <inheritdoc />
    public async Task<int> AddAppointmentSlotAsync(
        AddAppoinmentSlotCommand command,
        CancellationToken cancellationToken = default)
    {
        await modelValidator.ValidateAndThrowAsync(command);

        var appSlot = mapper.Map<AppointmentSlot>(command);
        return await appointmentSlotsRepository.AddAsync(appSlot);
    }

    /// <inheritdoc />
    public async Task<bool> AddAppointmentSlotsByTemplate(
        AddAppointmentSlotsByTemplateCommand command,
        CancellationToken cancellationToken = default)
    {
        await modelValidator.ValidateAndThrowAsync(command);

        var calculatedSlots = appointmentSlotsCalculator.CalculateSlots(
            new AppointmentSlotsCalculationContext(
                command.PolyclinicId,
                command.DoctorIds,
                command.PeriodStartDate,
                command.PeriodEndDate,
                command.AppointmentDuration,
                command.WorkDayStartTime,
                command.WorkDayEndTime,
                command.LunchDuration,
                command.IncludedWeekendDays));

        return await appointmentSlotsRepository
            .AddBatchAsync(calculatedSlots.Select(mapper.Map<AppointmentSlot>).ToArray());
    }

    /// <inheritdoc />
    public async Task UpdateAppointmentSlotAsync(
        UpdateAppointmentSlotCommand command,
        CancellationToken cancellationToken = default)
    {
        await modelValidator.ValidateAndThrowAsync(command);

        var newEntity = mapper.Map<AppointmentSlot>(command);
        await appointmentSlotsRepository.UpdateAsync(newEntity);
    }

    /// <inheritdoc />
    public async Task UpdateAppointmentSlotStatusAsync(
        UpdateAppointmentSlotStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        await modelValidator.ValidateAndThrowAsync(command);

        var insertedSlot = await appointmentSlotsRepository.GetByIdAsync(command.AppointmentSlotId);
        if (insertedSlot is null)
        {
            throw new EntryNotFoundException("Слот приёма к врачу не найден");
        }

        insertedSlot.Status = command.Status;
        await appointmentSlotsRepository.UpdateAsync(insertedSlot);
    }

    /// <inheritdoc />
    public async Task DeleteAppointmentSlotAsync(int id, CancellationToken cancellationToken = default) =>
        await appointmentSlotsRepository.DeleteAsync(id);

    /// <inheritdoc />
    public async Task DeletePolyclinicAppointmentSlotsByFilterAsync(
        DeletePolyclinicAppointmentSlotsByFilterCommand command,
        CancellationToken cancellationToken = default)
    {
        await modelValidator.ValidateAndThrowAsync(command);

        await appointmentSlotsRepository.DeleteByFilterAsync(
            s =>
                (command.DoctorId == null || s.DoctorId == command.DoctorId) &&
                (command.PolyclinicId == null || s.PolyclinicId == command.PolyclinicId) &&
                ((command.PeriodStartDate == null && command.PeriodEndDate == null) || s.Date >= command.PeriodStartDate && s.Date <= command.PeriodEndDate));
    }

    /// <inheritdoc />
    public async Task<AppointmentSlotDto?> GetAppointmentSlotByIdAsync(int id, CancellationToken cancellationToken = default) =>
        mapper.Map<AppointmentSlotDto?>(await appointmentSlotsRepository.GetByIdAsync(id));

    /// <inheritdoc />
    public async Task<AppointmentSlotDto[]> GetPolyclinicAppointmentSlotsByDateAsync(
        PolyclinicAppointmentSlotsByDateCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await modelValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return [];
        }

        var result = await appointmentSlotsRepository.GetByFilterAsync(s =>
            s.PolyclinicId == command.PolyclinicId &&
            s.Date >= command.Date.Date &&
            s.Date <= command.Date.Date.AddDays(1)) ?? [];

        return result.Select(mapper.Map<AppointmentSlotDto>).ToArray();
    }

    /// <inheritdoc />
    public async Task<AppointmentSlotDto[]> GetDoctorActiveAppointmentSlotsAsync(
        DoctorActiveAppointmentSlotsCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await modelValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return [];
        }

        var result = await appointmentSlotsRepository.GetByFilterAsync(s =>
            s.DoctorId == command.DoctorId &&
            (command.PolyclinicId == null || s.PolyclinicId == command.PolyclinicId) &&
            (command.Date == null || (s.Date >= command.Date.Value.Date && s.Date <= command.Date.Value.Date.AddDays(1))) &&
            s.Status != AppointmentSlotStatus.Closed) ?? [];

        return result.Select(mapper.Map<AppointmentSlotDto>).ToArray();
    }

    /// <inheritdoc />
    public async Task<AppointmentSlotDto[]> GetPatientAppointmentSlotsAsync(
        GetPatientAppointmentSlotsCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await modelValidator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return [];
        }

        var result = await appointmentSlotsRepository.GetByFilterAsync(s =>
            s.UserId == command.PatientId &&
            (command.StartDate == null || s.Date >= command.StartDate) &&
            (command.EndDate == null || s.Date <= command.EndDate)) ?? [];

        return result.Select(mapper.Map<AppointmentSlotDto>).ToArray();
    }

    /// <inheritdoc />
    public async Task SlotReservationAsync(
        UserSlotReservationCommand command,
        CancellationToken cancellationToken = default)
    {
        await modelValidator.ValidateAndThrowAsync(command);

        var slot = await appointmentSlotsRepository.GetByIdAsync(command.SlotId) ??
            throw new EntryNotFoundException("Слот приёма к врачу не найден.");

        if (slot.UserId != null)
        {
            throw new InvalidOperationException("Слот уже занят.");
        }

        slot.UserId = command.UserId;

        await appointmentSlotsRepository.UpdateAsync(slot);

        if (command.IssuePermitOfMetrics)
        {
            var metricAccessRequest = new AccessToMetricsCreateDTO
            {
                ProviderUserId = command.UserId,
                GrantedUserId = slot.DoctorId,
                AccessExpirationDate = DateOnly.FromDateTime(slot.Date.AddDays(1)), // добавляем день доступа от даты приема для врача
                                                                                    // например, для доступа к анализам, которые были сданы после приема
                IsPermanentAccess = false
            };

            await metricServiceClient.CreateAccessToMetricsAsync(metricAccessRequest);
        }
    }
}