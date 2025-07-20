using FluentValidation;

namespace PolyclinicService.BLL.Common.Extensions;

internal static class FluentValidationExtension
{
    public static IRuleBuilderOptions<T, string?> UrlAddress<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder.Must(ValidateUrl);

    public static IRuleBuilderOptions<T, string?> EmailAddress<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public static IRuleBuilderOptions<T, string?> PhoneNumber<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder.Matches(@"^\+?[0-9]{10,15}$");

    private static bool ValidateUrl(string? url) => 
        Uri.TryCreate(url, UriKind.Absolute, out var result) &&
        (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
}