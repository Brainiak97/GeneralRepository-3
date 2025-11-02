using FoodService.Api.Contracts;
using MetricService.Api.Contracts;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Auth;
using Shared.Logging;
using StateService.Api.Configuration;
using StateService.Api.Handlers;
using StateService.Api.Mapping;
using StateService.BLL.Interfaces;
using StateService.BLL.Mapping;
using StateService.DAL.Interfaces;
using StateService.DAL.Providers;

var builder = WebApplication.CreateBuilder(args);

// Настройка логгера ДО построения хоста
LoggingConfiguration.ConfigureLogger(
    serviceName: "StateService",
    layer: "API",
    builder.Configuration,
    environment: builder.Environment.EnvironmentName);

builder.Host.UseSerilog(); // Важно: подключить Serilog как провайдер

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(x => x.AddProfiles([new MapperProfile(), new ApiMapperProfile()]));

// Загрузка общей конфигурации JWT
builder.Services.AddJwtAuthentication();

builder.Services.AddScoped<IStateService, StateService.BLL.Services.StateService>();

builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("Services"));
var serviceUrlsFromConfig = builder.Configuration.GetSection("Services").Get<ServiceUrls>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

if (serviceUrlsFromConfig != null)
{
    builder.Services.AddTransient<DelegatingHandler, AuthHeaderHandler>();
    
    builder.Services.AddFoodServiceClient(serviceUrlsFromConfig.FoodServiceUrl);
    builder.Services.AddMetricServiceClient(serviceUrlsFromConfig.MetricServiceUrl);
   
    builder.Services.AddHttpClient<IGroqProvider, HttpGroqProvider>(client =>
    {
        client.BaseAddress = new Uri(serviceUrlsFromConfig.GroqUrl);
    });

    builder.Services.AddScoped<IHeaderDictionary>(x => x.GetRequiredService<IHttpContextAccessor>().HttpContext!.Request.Headers);
}

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "StateService API", Version = "v1" });
    // Добавляем схему JWT в Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCommonRequestLogging();

app.UseMiddleware<CorrelationIdMiddleware>(); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
