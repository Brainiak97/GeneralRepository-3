using EmailService.DAL.EF;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
