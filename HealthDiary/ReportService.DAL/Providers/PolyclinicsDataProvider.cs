using ReportService.DAL.Interfaces.Providers;

namespace ReportService.DAL.Providers;

/// <inheritdoc />
internal class PolyclinicsDataProvider(HttpClient httpClient) : IPolyclinicsDataProvider
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    /// <inheritdoc />
    public Task<object> GetAppointmentResultById(int appResultId)
    {
        throw new NotImplementedException();
    }
}