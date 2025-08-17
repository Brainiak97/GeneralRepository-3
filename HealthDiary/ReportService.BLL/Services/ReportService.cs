using System.Text.Json;
using ReportService.Api.Contracts.Data.Dto;
using ReportService.Api.Contracts.Data.Responses;
using ReportService.Api.Contracts.Enums;
using ReportService.BLL.Common.ReportFactory;
using ReportService.BLL.Interfaces;
using ReportService.DAL.Interfaces.Providers;
using Shared.Common.Extensions;

namespace ReportService.BLL.Services;

/// <inheritdoc />
internal class ReportService(
    IReportGeneratorFactory reportGeneratorFactory,
    IPolyclinicsDataProvider polyclinicsDataProvider) : IReportService
{
    /// <inheritdoc />
    public async Task<GenerateReportResponse> GenerateReportAsync(int appointmentResultId, ReportFormat reportFormat)
    {
        if (appointmentResultId <= 0)
        {
            throw new ArgumentException("Некорректный идентификатор результата приёма врача");
        }

        var appointmentResult = await polyclinicsDataProvider.GetAppointmentResultById(appointmentResultId);
        if (appointmentResult is null)
        {
            throw new InvalidOperationException(
                $"Ошибка получения результата приёма у врача по идентификатору {appointmentResultId}");
        }

        // var testJson = new CardiologistReportDataDto
        // {
        //     AppointmentDate = new DateTime(2025, 8, 12),
        //     DoctorFullName = "Иванов И.И.",
        //     PatientFullName = "Петров П.П.",
        //     PatientBirthDate = new DateOnly(1980, 8, 12),
        //     Anamnesis = "Неплохо",
        //     IsSmoker = false,
        //     ElectrocardiographyResult = string.Empty,
        //     EchocardiographyResult = string.Empty,
        //     SkinState = "Удовлетворительно",
        //     BloodPressureSys = 120,
        //     BloodPressureDia = 80,
        //     HeartRate = 70,
        //     BreathingLungs = "Ритмичное",
        //     HasWheeze = false,
        //     RespiratoryRate = 18,
        //     HasSwellings = false,
        //     PatientWeight = 71,
        //     PatientHeight = 178,
        //     Bmi = 34,
        //     Diagnosis = "Пока не понял",
        //     TreatmentPlan = "Потом посмотрим",
        //     MedicalExaminationAppointments = string.Empty,
        // };

        var reportGenerator = reportGeneratorFactory.CreateGenerator(reportFormat);
        var report = await reportGenerator.GenerateAsync(1, JsonSerializer.Serialize(testJson));

        return new GenerateReportResponse(report, $"report_{DateTime.Now:yyyyMMdd_HHmmss}.{reportFormat.GetDisplayName()}");
    }
}