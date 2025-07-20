using Microsoft.EntityFrameworkCore;
using Shared.Common.Interfaces;

namespace PolyclinicService.Domain.Models.Entities;

/// <summary>
/// Врач.
/// </summary>
[Comment("Врачи поликлиник")]
public class Doctor : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор врача в БД (UserId в UserService).
    /// </summary>
    [Comment("Идентификатор врача")]
    public required int Id { get; set; }

    /// <summary>
    /// Стаж врача (лет).
    /// </summary>
    [Comment("Стаж врача")]
    public byte Seniority { get; set; }

    /// <summary>
    /// Квалификация врача.
    /// </summary>
    [Comment("Квалификация врача")]
    public QualificationType QualificationType { get; set; }

    /// <summary>
    /// Научная степень врача (если есть).
    /// </summary>
    [Comment("Научная степень врача")]
    public AcademyDegree? AcademyDegree { get; set; }

    /// <summary>
    /// Признак, что у врача подтвержден документ об образовании.
    /// </summary>
    [Comment("Признак, что у врача подтвержден документ об образовании")]
    public bool IsConfirmedEducation { get; set; }

    /// <summary>
    /// Признак, что у врача подтвержден документ о квалификации.
    /// </summary>
    [Comment("Признак, что у врача подтвержден документ о квалификации")]
    public bool IsConfirmedQualification { get; set; }

    /// <summary>
    /// Специализация врача.
    /// </summary>
    [Comment("Специализация врача")]
    public SpecializationType SpecializationType { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с поликлиникой.
    /// </summary>
    public ICollection<Polyclinic> Polyclinics { get; set; } = null!;
}