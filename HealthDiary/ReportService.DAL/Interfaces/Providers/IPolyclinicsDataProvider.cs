namespace ReportService.DAL.Interfaces.Providers;

public interface IPolyclinicsDataProvider
{
    /// <summary>
    /// Вернуть результат приёма врача.
    /// </summary>
    /// <param name="appResultId">Идентификатор результат приёма врача.</param>
    /// <returns>Данные по результату приёма врача.</returns>
    Task<object> GetAppointmentResultById(int appResultId);
}