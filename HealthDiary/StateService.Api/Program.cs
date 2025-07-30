using Microsoft.OpenApi.Models;
using Shared.Auth;
using StateService.Api.Configuration;
using StateService.BLL.Interfaces;
using StateService.DAL.Interfaces;
using StateService.DAL.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Загрузка общей конфигурации JWT
builder.Services.AddJwtAuthentication();

builder.Services.AddScoped<IStateService, StateService.BLL.Services.StateService>();

builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("Services"));
var serviceUrlsFromConfig = builder.Configuration.GetSection("Services").Get<ServiceUrls>();

if (serviceUrlsFromConfig != null)
{
    builder.Services.AddHttpClient<IUserDataProvider, HttpUserDataProvider>(client =>
    {
        client.BaseAddress = new Uri(serviceUrlsFromConfig.UserServiceUrl);
    });
    builder.Services.AddHttpClient<IFoodDataProvider, HttpFoodDataProvider>(client =>
    {
        client.BaseAddress = new Uri(serviceUrlsFromConfig.FoodServiceUrl);
    });
    builder.Services.AddHttpClient<IMetricDataProvider, HttpMetricDataProvider>(client =>
    {
        client.BaseAddress = new Uri(serviceUrlsFromConfig.MetricServiceUrl);
    });
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
