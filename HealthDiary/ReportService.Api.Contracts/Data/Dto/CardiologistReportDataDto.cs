using System.ComponentModel.DataAnnotations;
using ReportService.Api.Contracts.Data.Interfaces;

namespace ReportService.Api.Contracts.Data.Dto;

/// <summary>
/// Данные отчёта приёма у кардиолога.
/// </summary>
[Display(Name = "Отчёт кардиолога")]
public record CardiologistReportDataDto : IReportData
{
    /// <summary>
    /// Дата приёма.
    /// </summary>
    [Display(Name = "Дата")]
    public DateTime AppointmentDate { get; init; }

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
    /// Признак: пациент - курильщик.
    /// </summary>
    [Display(Name = "Курение")]
    public bool IsSmoker { get; init; }

    /// <summary>
    /// Результат ЭКГ (если есть).
    /// </summary>
    [Display(Name = "Результат ЭКГ")]
    public string ElectrocardiographyResult { get; init; } = string.Empty;

    /// <summary>
    /// Результат ЭХО (если есть).
    /// </summary>
    [Display(Name = "Результат ЭХО")]
    public string EchocardiographyResult { get; init; } = string.Empty;

    /// <summary>
    /// Состояние кожи.
    /// </summary>
    [Display(Name = "Состояние кожи")]
    public string SkinState { get; init; } = string.Empty;

    /// <summary>
    /// Верх. арт. давление (мм. рт. ст.).
    /// </summary>
    [Display(Name = "Верх. арт. давление (мм. рт. ст.)")]
    public short BloodPressureSys { get; init; }

    /// <summary>
    /// Ниж. арт. давление (мм. рт. ст.).
    /// </summary>
    [Display(Name = "Ниж. арт. давление (мм. рт. ст.)")]
    public short BloodPressureDia { get; init; }

    /// <summary>
    /// Частота сердечных сокращений.
    /// </summary>
    [Display(Name = "ЧСС")]
    public short HeartRate { get; init; }

    /// <summary>
    /// Дыхание в лёгких.
    /// </summary>
    [Display(Name = "Дыхание в лёгких")]
    public string BreathingLungs { get; init; } = string.Empty;

    /// <summary>
    /// Признак: обнаружены хрипы при дыхании.
    /// </summary>
    [Display(Name = "Наличие хрипов")]
    public bool HasWheeze { get; init; }

    /// <summary>
    /// Частота дыхательных движений.
    /// </summary>
    [Display(Name = "ЧДД")]
    public short RespiratoryRate { get; init; }

    /// <summary>
    /// Признак наличия отёков.
    /// </summary>
    [Display(Name = "Наличие отёков")]
    public bool HasSwellings { get; init; }

    /// <summary>
    /// Масса тела в кг.
    /// </summary>
    [Display(Name = "Масса тела в кг")]
    public float PatientWeight { get; init; }

    /// <summary>
    /// Рост в сантиметрах.
    /// </summary>
    [Display(Name = "Рост в сантиметрах")]
    public short PatientHeight { get; init; }

    /// <summary>
    /// Индекс массы тела.
    /// </summary>
    [Display(Name = "ИМТ в кг/м2")]
    public double Bmi { get; init; }

    /// <summary>
    /// Диагноз.
    /// </summary>
    [Display(Name = "Диагноз")]
    public string Diagnosis { get; init; } = string.Empty;

    /// <summary>
    /// Лечение.
    /// </summary>
    [Display(Name = "Лечение")]
    public string TreatmentPlan { get; init; } = string.Empty;

    /// <summary>
    /// План обследования.
    /// </summary>
    [Display(Name = "План обследования")]
    public string MedicalExaminationAppointments { get; init; } = string.Empty;
}