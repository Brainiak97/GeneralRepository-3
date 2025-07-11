using PolyclinicService.BLL.Data;
using PolyclinicService.BLL.Data.Dtos;

namespace PolyclinicService.BLL.Interfaces;

/// <summary>
/// Предоставляет методы для расчёта слотов приёмов к врачу.
/// </summary>
internal interface IAppointmentSlotsCalculator
{
    /// <summary>
    /// Рассчитать слоты приёмов к врачу.
    /// </summary>
    /// <param name="context">Контекст расчёта слотов приёмов к врачу.</param>
    /// <returns>Слоты приёмов к врачу поликлиники.</returns>
    IEnumerable<AppointmentSlotDto> CalculateSlots(AppointmentSlotsCalculationContext context);
}