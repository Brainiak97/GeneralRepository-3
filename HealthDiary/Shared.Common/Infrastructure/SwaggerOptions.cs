using Microsoft.OpenApi.Models;

namespace Shared.Common.Infrastructure;

/// <summary>
/// Данные для формирования документации Swagger.
/// </summary>
public class SwaggerOptions
{
    /// <summary>
    /// REQUIRED. The title of the application.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// A short description of the application.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// REQUIRED. The version of the OpenAPI document.
    /// </summary>
    public required string Version { get; set; }

    /// <summary>
    /// A URL to the Terms of Service for the API. MUST be in the format of a URL.
    /// </summary>
    public Uri TermsOfService { get; set; } = null!;

    /// <summary>
    /// The contact information for the exposed API.
    /// </summary>
    public OpenApiContact Contact { get; set; } = null!;

    /// <summary>
    /// The license information for the exposed API.
    /// </summary>
    public OpenApiLicense License { get; set; } = null!;
}