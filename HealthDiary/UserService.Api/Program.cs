using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared.Auth;
using Shared.EmailClient;
using UserService.Api.Data;
using UserService.BLL.Interfaces;
using UserService.DAL.EF;
using UserService.DAL.Interfaces;
using UserService.DAL.Repositories;
using UserService.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// ���������� ���������
builder.Services.AddDbContext<UserServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(true);
});

// Email ������
builder.Services.AddEmailServiceClient("https://localhost:7281/");

// �������� ����� ������������ JWT
builder.Services.AddJwtAuthentication();

// ����������� �������� � ������������
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService.BLL.Services.UserService>();
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<IJwtService, JwtService>();

// ��������� �������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ��������� �������
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService API", Version = "v1" });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UserService.Api.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UserService.BLL.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UserService.DAL.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UserService.Domain.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Shared.Auth.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Shared.EmailClient.xml"));
    // ��������� ����� JWT � Swagger
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

        // ���������� ��������
        await context.Database.MigrateAsync();

        // ������������� ����� � ������
        await InitializeDatabase.Initialize(services, new PasswordHasher<User>());
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "������ ��� ������������� ��");
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
