using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Interfaces;

namespace PolyclinicService.Domain.Models.Entities;

/// <summary>
/// Данные о поликлинике.
/// </summary>
[Comment("Поликлиники")]
public class Polyclinic : IEntityModel<int>
{
    /// <summary>
    /// Идентификатор поликлиники.
    /// </summary>
    [Comment("Идентификатор поликлиники")]
    public int Id { get; set; }

    /// <summary>
    /// Наименование поликлиники.
    /// </summary>
    [Comment("Наименование поликлиники")]
    [MaxLength(255)]
    public required string Name { get; set; }

    /// <summary>
    /// Адрес.
    /// </summary>
    [Comment("Адрес")]
    [MaxLength(255)]
    public required string Address { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    [Comment("Номер телефона")]
    [MaxLength(15)]
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    [Comment("Адрес электронной почты")]
    [MaxLength(100)]
    public string? Email { get; set; }

    /// <summary>
    /// Ссылка на сайт.
    /// </summary>
    [Comment("Ссылка на сайт")]
    [MaxLength(500)]
    public string? Url { get; set; }

    /// <summary>
    /// Доктора работающие в поликлинике.
    /// </summary>
    public ICollection<Doctor> Doctors { get; set; } = null!;
}