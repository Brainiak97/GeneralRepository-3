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
    public string Title { get; set; }

    /// <summary>
    /// A short description of the application.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// REQUIRED. The version of the OpenAPI document.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// A URL to the Terms of Service for the API. MUST be in the format of a URL.
    /// </summary>
    public Uri TermsOfService { get; set; }

    /// <summary>
    /// The contact information for the exposed API.
    /// </summary>
    public OpenApiContact Contact { get; set; }

    /// <summary>
    /// The license information for the exposed API.
    /// </summary>
    public OpenApiLicense License { get; set; }
}