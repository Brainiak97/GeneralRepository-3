using PolyclinicService.Api.Controllers;

namespace PolyclinicService.Api.WebRoutes;

/// <summary>
/// Маршруты до методов контроллера <see cref="PolyclinicSchedulesController"/>.
/// </summary>
internal static class PolyclinicSchedulesControllerWebRoutes
{
    /// <summary>
    /// Базовая часть маршрута до методов PolyclinicSchedulesController.
    /// </summary>
    public const string BasePath = "api/polyclinic-schedules";
    
    /// <summary>
    /// Маршрут до метода AddAppointmentSlot.
    /// </summary>
    public const string AddAppointmentSlotRoute = "add-appointment-slot";
    
    /// <summary>
    /// Маршрут до метода AddAppointmentSlotsByTemplate.
    /// </summary>
    public const string AddAppointmentSlotsByTemplateRoute = "add-appointment-slots-by-template";

    /// <summary>
    /// Маршрут до метода UpdateAppointmentSlot.
    /// </summary>
    public const string UpdateAppointmentSlotRoute = "update-appointment-slot";
    
    /// <summary>
    /// Маршрут до метода UpdateAppointmentSlotStatus.
    /// </summary>
    public const string UpdateAppointmentSlotStatusRoute = "update-appointment-slot-status";
    
    /// <summary>
    /// Маршрут до метода DeleteAppointmentSlot.
    /// </summary>
    public const string DeleteAppointmentSlotRoute = "delete-appointment-slot/{id:int}";
    
    /// <summary>
    /// Маршрут до метода DeletePolyclinicAppointmentSlotsByFilter.
    /// </summary>
    public const string DeletePolyclinicAppointmentSlotsByFilterRoute = "delete-polyclinic-appointment-slot-by-filter";
    
    /// <summary>
    /// Маршрут до метода GetPolyclinicAppointmentSlotsByDate.
    /// </summary>
    public const string GetPolyclinicAppointmentSlotsByDateRoute = "get-polyclinic-appointment-slot-by-date";
    
    /// <summary>
    /// Маршрут до метода GetDoctorActiveAppointmentSlots.
    /// </summary>
    public const string GetDoctorActiveAppointmentSlotsRoute = "get-doctor-active-appointment-slots";
}