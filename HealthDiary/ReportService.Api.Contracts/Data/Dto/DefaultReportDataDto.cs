using System.ComponentModel.DataAnnotations;
using ReportService.Api.Contracts.Data.Interfaces;

namespace ReportService.Api.Contracts.Data.Dto;

/// <summary>
/// Данные отчёта приёма у кардиолога.
/// </summary>
[Display(Name = "Cтандартный отчёт")]
public record DefaultReportDataDto : IReportData
{
    /// <summary>
    /// Дата приёма.
    /// </summary>
    [Display(Name = "Дата")]
    public DateOnly AppointmentDate { get; init; }

    /// <summary>
    /// ФИО врача.
    /// </summary>
    [Display(Name = "ФИО врача")]
    public required string DoctorFullName { get; init; }

    /// <summary>
    /// ФИО пациента.
    /// </summary>
    [Display(Name = "ФИО пациента")]
    public required string PatientFullName { get; init; }

    /// <summary>
    /// Дата рождения пациента.
    /// </summary>
    [Display(Name = "Дата рождения пациента")]
    public DateOnly PatientBirthDate { get; init; }

    /// <summary>
    /// Анамнез.
    /// </summary>
    [Display(Name = "Анамнез")]
    public string Anamnesis { get; init; } = string.Empty;

    /// <summary>
    /// Диагноз.
    /// </summary>
    [Display(Name = "Диагноз")]
    public string Diagnosis { get; init; } = string.Empty;

    /// <summary>
    /// План обследования.
    /// </summary>
    [Display(Name = "План обследования")]
    public string MedicalExaminationAppointments { get; init; } = string.Empty;

    /// <summary>
    /// Лечение.
    /// </summary>
    [Display(Name = "Лечение")]
    public string TreatmentPlan { get; init; } = string.Empty;
}