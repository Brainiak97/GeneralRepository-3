using PolyclinicService.Api.Controllers;

namespace PolyclinicService.Api.WebRoutes;

/// <summary>
/// Маршруты до методов контроллера <see cref="DoctorsController"/>.
/// </summary>
internal static class DoctorsControllerWebRoutes
{
    /// <summary>
    /// Базовая часть маршрута до методов DoctorsController.
    /// </summary>
    public const string BasePath = "api/doctors";
    
    /// <summary>
    /// Маршрут до метода GetDoctors.
    /// </summary>
    public const string GetDoctorsRoute = "all";
    
    /// <summary>
    /// Маршрут до метода GetPolyclinicDoctors.
    /// </summary>
    public const string GetPolyclinicDoctorsRoute = "get-polyclinic-doctors/{polyclinicId:int}";
    
    /// <summary>
    /// Маршрут до метода AddDoctor.
    /// </summary>
    public const string AddDoctorRoute = "add-doctor";
    
    /// <summary>
    /// Маршрут до метода UpdateDoctorInfo.
    /// </summary>
    public const string UpdateDoctorInfoRoute = "update-doctor-info";
    
    /// <summary>
    /// Маршрут до метода DeleteDoctor.
    /// </summary>
    public const string DeleteDoctorRoute = "delete-doctor/{id:int}";
}