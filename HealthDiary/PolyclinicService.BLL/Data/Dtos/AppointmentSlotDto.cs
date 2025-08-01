using PolyclinicService.Domain.Models;

namespace PolyclinicService.BLL.Data.Dtos;

/// <summary>
/// Класс передачи данных по слоту приёма в графике поликлиники.
/// </summary>
public class AppointmentSlotDto
{
    /// <summary>
    /// Идентификатор приёма в графике.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор врача.
    /// </summary>
    public int DoctorId { get; set; }
    
    /// <summary>
    /// Идентификатор поликлинники.
    /// </summary>
    public int PolyclinicId { get; set; }

    /// <summary>
    /// Идентификатор пользователя (пациента).
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Дата приёма.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Продолжительность приёма.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Статус приёма в графике.
    /// </summary>
    public AppointmentSlotStatus Status { get; set; }    
}