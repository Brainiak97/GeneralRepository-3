using Microsoft.Extensions.DependencyInjection;
using Shared.EmailService.Common.Builders;

namespace Shared.EmailService.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailMessageBuilder(this IServiceCollection services) =>
        services.AddTransient<IEmailMessageRequestBuilder, EmailMessageBuilder>();
}