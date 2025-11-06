using AutoMapper;
using MetricService.Api.Contracts;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;
using PolyclinicService.BLL.Data;
using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;
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
    public async Task<int> AddAppointmentSlotAsync(AddAppoinmentSlotRequest request)
    {
        await modelValidator.ValidateAndThrowAsync(request);

        var appSlot = mapper.Map<AppointmentSlot>(request);
        return await appointmentSlotsRepository.AddAsync(appSlot);
    }

    /// <inheritdoc />
    public async Task<bool> AddAppointmentSlotsByTemplate(AddAppointmentSlotsByTemplateRequest request)
    {
        await modelValidator.ValidateAndThrowAsync(request);

        var calculatedSlots = appointmentSlotsCalculator.CalculateSlots(
            new AppointmentSlotsCalculationContext(
                request.PolyclinicId,
                request.DoctorIds,
                request.PeriodStartDate,
                request.PeriodEndDate,
                request.AppointmentDuration,
                request.WorkDayStartTime,
                request.WorkDayEndTime,
                request.LunchDuration,
                request.IncludedWeekendDays));

        return await appointmentSlotsRepository
            .AddBatchAsync(calculatedSlots.Select(mapper.Map<AppointmentSlot>).ToArray());
    }

    /// <inheritdoc />
    public async Task UpdateAppointmentSlotAsync(UpdateAppointmentSlotRequest request)
    {
        await modelValidator.ValidateAndThrowAsync(request);

        var insertedSlot = await appointmentSlotsRepository.GetByIdAsync(request.Id);
        if (insertedSlot is null)
        {
            throw new EntryNotFoundException("Слот приёма к врачу не найден");
        }

        insertedSlot.PolyclinicId = request.PolyclinicId ?? insertedSlot.PolyclinicId;
        insertedSlot.DoctorId = request.DoctorId ?? insertedSlot.DoctorId;
        insertedSlot.UserId = request.UserId ?? insertedSlot.UserId;
        insertedSlot.Date = request.Date ?? insertedSlot.Date;
        insertedSlot.Duration = request.Duration ?? insertedSlot.Duration;

        await appointmentSlotsRepository.UpdateAsync(insertedSlot);
    }

    /// <inheritdoc />
    public async Task UpdateAppointmentSlotStatusAsync(UpdateAppointmentSlotStatusRequest request)
    {
        await modelValidator.ValidateAndThrowAsync(request);

        var insertedSlot = await appointmentSlotsRepository.GetByIdAsync(request.AppointmentSlotId);
        if (insertedSlot is null)
        {
            throw new EntryNotFoundException("Слот приёма к врачу не найден");
        }

        insertedSlot.Status = request.Status;
        await appointmentSlotsRepository.UpdateAsync(insertedSlot);
    }

    /// <inheritdoc />
    public async Task DeleteAppointmentSlotAsync(int id) =>
        await appointmentSlotsRepository.DeleteAsync(id);

    /// <inheritdoc />
    public async Task DeletePolyclinicAppointmentSlotsByFilterAsync(DeletePolyclinicAppointmentSlotsByFilterRequest request)
    {
        await modelValidator.ValidateAndThrowAsync(request);

        await appointmentSlotsRepository.DeleteByFilterAsync(
            s =>
                (request.DoctorId == null || s.DoctorId == request.DoctorId) &&
                (request.PolyclinicId == null || s.PolyclinicId == request.PolyclinicId) &&
                ((request.PeriodStartDate == null && request.PeriodEndDate == null) || s.Date >= request.PeriodStartDate && s.Date <= request.PeriodEndDate));
    }

    /// <inheritdoc />
    public async Task<AppointmentSlotDto?> GetAppointmentSlotByIdAsync(int id) =>
        mapper.Map<AppointmentSlotDto?>(await appointmentSlotsRepository.GetByIdAsync(id));

    /// <inheritdoc />
    public async Task<AppointmentSlotDto[]> GetPolyclinicAppointmentSlotsByDateAsync(PolyclinicAppointmentSlotsByDateRequest request)
    {
        var validationResult = await modelValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return [];
        }

        var result = await appointmentSlotsRepository.GetByFilterAsync(s =>
            s.PolyclinicId == request.PolyclinicId &&
            s.Date >= request.Date.Date &&
            s.Date <= request.Date.Date.AddDays(1)) ?? [];

        return result.Select(mapper.Map<AppointmentSlotDto>).ToArray();
    }

    /// <inheritdoc />
    public async Task<AppointmentSlotDto[]> GetDoctorActiveAppointmentSlotsAsync(DoctorActiveAppointmentSlotsRequest request)
    {
        var validationResult = await modelValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return [];
        }

        var result = await appointmentSlotsRepository.GetByFilterAsync(s =>
            s.DoctorId == request.DoctorId &&
            (request.PolyclinicId == null || s.PolyclinicId == request.PolyclinicId) &&
            (request.Date == null || (s.Date >= request.Date.Value.Date && s.Date <= request.Date.Value.Date.AddDays(1))) &&
            s.Status != AppointmentSlotStatus.Closed) ?? [];

        return result.Select(mapper.Map<AppointmentSlotDto>).ToArray();
    }

    /// <inheritdoc />
    public async Task SlotReservationAsync(UserSlotReservationRequest request)
    {
        await modelValidator.ValidateAndThrowAsync(request);

        var slot = await appointmentSlotsRepository.GetByIdAsync(request.SlotId) ??
            throw new EntryNotFoundException("Слот приёма к врачу не найден.");

        if (slot.UserId != null)
        {
            throw new InvalidOperationException("Слот уже занят.");
        }

        slot.UserId = request.UserId;

        await appointmentSlotsRepository.UpdateAsync(slot);

        if (request.IssuePermitOfMetrics)
        {
            var metricAccessRequest = new AccessToMetricsCreateDTO
            {
                ProviderUserId = request.UserId,
                GrantedUserId = slot.DoctorId,
                AccessExpirationDate = DateOnly.FromDateTime(slot.Date.AddDays(1)), // добавляем день доступа от даты приема для врача
                                                                                    // например, для доступа к анализам, которые были сданы после приема
                IsPermanentAccess = false
            };

            await metricServiceClient.CreateAccessToMetricsAsync(metricAccessRequest);
        }
    }
}