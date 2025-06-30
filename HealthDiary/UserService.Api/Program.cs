using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared.Auth;
using Shared.EmailClient;
using UserService.Api.Data;
using UserService.Api.Infrastructure;
using UserService.BLL;
using UserService.BLL.Interfaces;
using UserService.DAL.EF;
using UserService.DAL.Interfaces;
using UserService.DAL.Repositories;
using UserService.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавление контекста
builder.Services.AddDbContext<UserServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(true);
});

// Настройка JSON-сериализатора
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new UserServiceDateTimeConverter());
});

// Регистрируем AutoMapper из BLL
builder.Services.AddAutoMapper(typeof(MapperProfile));

// Email сервис
builder.Services.AddEmailServiceClient("https://localhost:7281/");

// Загрузка общей конфигурации JWT
builder.Services.AddJwtAuthentication();

// Регистрация сервисов и репозиториев
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService.BLL.Services.UserService>();
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<IJwtService, JwtService>();

// Остальные сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Настройка свагера
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService API", Version = "v1" });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UserService.Api.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UserService.BLL.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UserService.DAL.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UserService.Domain.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Shared.Auth.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Shared.EmailClient.xml"));

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<UserServiceDbContext>();

        // Применение миграций
        await context.Database.MigrateAsync();

        // Инициализация ролей и админа
        await InitializeDatabase.Initialize(services, new PasswordHasher<User>());
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при инициализации БД");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
