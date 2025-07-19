using PolyclinicService.Api.Infrastructure;
using PolyclinicService.BLL.Infrastructure;
using PolyclinicService.DAL.Infrastructure;
using Shared.Auth;
using Shared.Common.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication();

builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var swaggerOptions = builder.Configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>();
var serviceName = builder.Configuration.GetValue<string>("ServiceName");
builder.Services.AddSwagger(swaggerOptions, serviceName);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitializeServiceDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();