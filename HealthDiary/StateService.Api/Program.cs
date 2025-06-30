using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

builder.Services.AddScoped<IStateService, StateService.BLL.Services.StateService>();

builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("Services"));
var serviceUrlsFromConfig = builder.Configuration.GetSection("Services").Get<ServiceUrls>();

builder.Services.AddHttpClient<IUserDataProvider, HttpUserDataProvider>(client =>
{
    client.BaseAddress = new Uri(serviceUrlsFromConfig?.UserService);
}); 
builder.Services.AddHttpClient<IFoodDataProvider, HttpFoodDataProvider>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:FoodService:Url"]);
}); 
builder.Services.AddHttpClient<IMetricDataProvider, HttpMetricDataProvider>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:MetricService:Url"]);
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
