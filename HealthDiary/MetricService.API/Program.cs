using MetricService.DAL.EF;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MetricService.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ���������� ���������
            builder.Services.AddDbContext<MetricServiceDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(false);                
            });

            // ��������� �������
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();



            // ��������� �������            
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricService API", Version = "v1" });
            });

            builder.Services.AddControllers();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MetricServiceDbContext>();

                    // ���������� ��������
                     await context.Database.MigrateAsync();    
                    
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "������ ��� ������������� ��");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
