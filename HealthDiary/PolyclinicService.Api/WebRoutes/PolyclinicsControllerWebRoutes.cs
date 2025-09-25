using PolyclinicService.Api.Controllers;

namespace PolyclinicService.Api.WebRoutes;

/// <summary>
/// Маршруты до методов контроллера <see cref="PolyclinicsController"/>.
/// </summary>
internal static class PolyclinicsControllerWebRoutes
{
    /// <summary>
    /// Маршрут до метода GetPolyclinics.
    /// </summary>
    public const string GetPolyclinicsRoute = "all";

    /// <summary>
    /// Маршрут до метода AddPolyclinic.
    /// </summary>
    public const string AddPolyclinicRoute = "add-polyclinic";

    /// <summary>
    /// Маршрут до метода UpdatePolyclinicInfo.
    /// </summary>
    public const string UpdatePolyclinicInfoRoute = "update-polyclinic-info";

    /// <summary>
    /// Маршрут до метода DeletePolyclinic.
    /// </summary>
    public const string DeletePolyclinicRoute = "delete-doctor/{id:int}";
}