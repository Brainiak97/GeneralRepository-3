using EmailService.BLL.Interfaces;
using EmailService.BLL.Services;
using EmailService.DAL.EF;
using EmailService.DAL.Interfaces;
using EmailService.DAL.Repositories;
using EmailService.Domain.Models;
using EmailService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared.Auth;

var builder = WebApplication.CreateBuilder(args);

// Добавление контекста
builder.Services.AddDbContext<EmailDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(true);
});

// Загрузка общей конфигурации JWT
builder.Services.AddJwtAuthentication();

// Конфигурация SMTP Service
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Регистрация сервисов и репозиториев
builder.Services.AddScoped<IRepository<EmailTemplate>, EmailTemplateRepository>();
builder.Services.AddScoped<IRepository<EmailLog>, EmailLogRepository>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

// Настройка свагера
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService API", Version = "v1" });

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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


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
