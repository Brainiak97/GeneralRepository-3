using System.Net.Http.Json;
using ReportService.DAL.Interfaces.Providers;
using ReportService.Domain.Dtos;

namespace ReportService.DAL.Providers;

/// <inheritdoc />
internal class PolyclinicsDataProvider(HttpClient httpClient) : IPolyclinicsDataProvider
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    /// <inheritdoc />
    public async Task<AppointmentResultDto?> GetAppointmentResultById(int appResultId)
    {
        var response = await _httpClient.GetAsync($"api/appointmentResults/{appResultId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Ошибка получения данных из сервиса поликлиник");
        }

        return await response.Content.ReadFromJsonAsync<AppointmentResultDto?>();
    }
}