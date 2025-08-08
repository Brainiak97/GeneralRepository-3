using ReportService.BLL.Infrastructure;
using ReportService.DAL.Infrastructure;
using Shared.Auth;
using Shared.Common.Infrastructure;

namespace ReportService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddJwtAuthentication();

        builder.Services.AddDataAccessServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        var swaggerOptions = builder.Configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>();
        builder.Services.AddSwagger(swaggerOptions, "ReportService");

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.Run();
    }
}