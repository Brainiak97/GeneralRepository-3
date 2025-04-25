using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class HealthDiaryDbContext(DbContextOptions<HealthDiaryDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
