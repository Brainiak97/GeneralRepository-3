namespace PolyclinicService.Api.WebRoutes;

/// <summary>
/// Маршруты до методов контроллера <see cref="AppointmentResultWebRoutes"/>.
/// </summary>
internal static class AppointmentResultWebRoutes
{
    /// <summary>
    /// Маршрут до метода SaveAppointmentResult.
    /// </summary>
    public const string SaveAppointmentResult = "save-appointment-result";

    /// <summary>
    /// Маршрут до метода UpdateAppointmentResult.
    /// </summary>
    public const string UpdateAppointmentResult = "update-appointment-result";

    /// <summary>
    /// Маршрут до метода DeleteAppointmentResult.
    /// </summary>
    public const string DeleteAppointmentResult = "{id:int}/delete";
}