using EmailService.Api.Contracts;
using QuestPDF.Infrastructure;
using ReportService.Api.Configurations;
using ReportService.Api.Mappers;
using ReportService.BLL.Infrastructure;
using ReportService.DAL.Contexts;
using ReportService.DAL.Infrastructure;
using Shared.Auth;
using Shared.Common.EFCore;
using Shared.Common.Infrastructure;
using Shared.Common.MessageBrokers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddJwtAuthentication();

QuestPDF.Settings.License = LicenseType.Community; // Без этого кидает Exception

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ReportServiceMapperProfile>());

builder.Services.AddOpenApi();

var swaggerOptions = builder.Configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>();
builder.Services.AddSwagger(swaggerOptions, "ReportService");

var serviceUrls = builder.Configuration.GetSection("ApiClients").Get<ServiceUrls>();
if (serviceUrls is not null)
{
    builder.Services.AddEmailServiceClient(serviceUrls.EmailServiceUrl);    
}


builder.Services.AddRabbitMq(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Services.ApplyDbMigration<ReportServiceDbContext>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();