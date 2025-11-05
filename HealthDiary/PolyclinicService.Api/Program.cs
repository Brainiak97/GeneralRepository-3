using PolyclinicService.Api.Mappers;
using PolyclinicService.BLL.Infrastructure;
using PolyclinicService.DAL.Infrastructure;
using PolyclinicService.DAL.Contexts;
using Shared.Auth;
using Shared.Common.EFCore;
using Shared.Common.Infrastructure;
using Shared.Common.Middlewares;
using MetricService.Api.Contracts;
using PolyclinicService.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication();

builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<PolyclinicServiceMapperProfile>());

builder.Services.AddOpenApi();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("Services"));
var serviceUrlsFromConfig = builder.Configuration.GetSection("Services").Get<ServiceUrls>();

if (serviceUrlsFromConfig != null)
{
    builder.Services.AddMetricServiceClient(serviceUrlsFromConfig.MetricServiceUrl);
}

builder.Services.AddScoped<IHeaderDictionary>(x => x.GetRequiredService<IHttpContextAccessor>().HttpContext!.Request.Headers);

var swaggerOptions = builder.Configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>();
builder.Services.AddSwagger(swaggerOptions, serviceName: "PolyclinicService");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Services.ApplyDbMigration<PolyclinicServiceDbContext>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();