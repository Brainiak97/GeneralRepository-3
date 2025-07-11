using Microsoft.EntityFrameworkCore;
using MetricService.Domain.Models;
using System.Reflection;

namespace MetricService.DAL.EF
{
    /// <summary>
    /// Контекст базы данных сервиса MetricService
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class MetricServiceDbContext(DbContextOptions<MetricServiceDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Набор данных базовых медицинских показателей пользователя
        /// </summary>       
        public DbSet<HealthMetricsBase> HealthMetricsBase { get; set; }

        /// <summary>
        /// Набор данных о параметрах сна пользователя
        /// </summary>       
        public DbSet<Sleep> Sleeps { get; set; }

        /// <summary>
        /// Набор данных о физической активности 
        /// </summary>       
        public DbSet<PhysicalActivity> PhysicalActivities { get; set; }

        /// <summary>
        /// Набор данных о тренировке пользователя
        /// </summary>       
        public DbSet<Workout> Workouts { get; set; }

        /// <summary>
        /// Набор данных о профиле пользователя
        /// </summary>       
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Надор данных о категории анализов
        /// </summary>        
        public DbSet<AnalysisCategory> AnalysisCategories { get; set; }

        /// <summary>
        /// Набор данных о типе анализов
        /// </summary>       
        public DbSet<AnalysisType> AnalysisTypes { get; set; }

        /// <summary>
        /// Набор данных о результате анализа пользователя
        /// </summary>        
        public DbSet<AnalysisResult> AnalysisResults { get; set; }

        /// <summary>
        /// Набор данных о медикаменте
        /// </summary>        
        public DbSet<Medication> Medications { get; set; }

        /// <summary>
        /// Набор данных о форме выпуска препарата
        /// </summary>       
        public DbSet<DosageForm> DosageForms { get; set; }

        /// <summary>
        /// Набор данных о схеме приема медикаментов
        /// </summary>        
        public DbSet<Regimen> Regimens { get; set; }

        /// <summary>
        /// Набор данных о приеме медикаментов пользователем
        /// </summary>       
        public DbSet<Intake> Intakes { get; set; }

        /// <summary>
        /// Набор данных о напоминании приема медикаментов пользователем
        /// </summary>      
        public DbSet<Reminder> Reminders { get; set; }

        /// <summary>
        /// Набор данных о доступе к личным метрикам пользователя
        /// </summary>       
        public DbSet<AccessToMetrics> AccessToMetrics { get; set; }


        /// <summary>
        /// Настройка контекста для моделей данных
        /// </summary>       
        /// <param name="modelBuilder">Объект, который определяет форму сущностей, связи между ними и способ их сопоставления с базой данных.</param>        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
