using PolyclinicService.Api.Infrastructure;
using PolyclinicService.BLL.Infrastructure;
using PolyclinicService.DAL.Infrastructure;
using Shared.Auth;
using Shared.Common.Infrastructure;
using Shared.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication();

builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddOpenApi();

var swaggerOptions = builder.Configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>();
builder.Services.AddSwagger(swaggerOptions, serviceName: "PolyclinicService");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitializeServiceDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();