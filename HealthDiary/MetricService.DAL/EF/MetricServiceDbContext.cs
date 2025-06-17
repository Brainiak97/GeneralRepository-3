using Microsoft.EntityFrameworkCore;
using MetricService.Domain.Models;
using System.Reflection;

namespace MetricService.DAL.EF
{
    public class MetricServiceDbContext(DbContextOptions<MetricServiceDbContext> options) : DbContext(options)
    {        
        public DbSet<HealthMetricsBase> HealthMetricsBase { get; set; }        
        public DbSet<Sleep> Sleeps { get; set; }
        public DbSet<PhysicalActivity> PhysicalActivities { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AnalysisCategory> AnalysisCategories { get; set; }
        public DbSet<AnalysisType> AnalysisTypes { get; set; }       
        public DbSet<AnalysisResult> AnalysisResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
           
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }        
    }
}
