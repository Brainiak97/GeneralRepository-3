using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о доступе к метрикам пользователя для других пользователей
    /// </summary>
    /// <seealso cref="BaseRepository&lt;AccessToMetrics&gt;" />
    /// <seealso cref="IAccessToMetricsRepository" />
    public class AccessToMetricsRepository : BaseRepository<AccessToMetrics>, IAccessToMetricsRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="AccessToMetricsRepository"/>.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public AccessToMetricsRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb) {}

        /// <inheritdoc/>       
        public async override Task<bool> UpdateAsync(AccessToMetrics item)
        {
            AccessToMetrics? accessToMetrics = await GetByIdAsync(item.Id);
            if (accessToMetrics != null)
            {
                accessToMetrics.AccessExpirationDate = item.AccessExpirationDate;
                accessToMetrics.IsPermanentAccess = item.IsPermanentAccess;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }

        /// <inheritdoc/>        
        public async Task<bool> CheckAccessToMetricsAsync(int providerUserId, int grantedUserId)
        {
          return await _contextDb.AccessToMetrics. Where(a => a.ProviderUserId == providerUserId &&
            a.GrantedUserId == grantedUserId &&
            (a.IsPermanentAccess == true || a.AccessExpirationDate >= DateOnly.FromDateTime(DateTime.Now))).AnyAsync();
        }

        /// <inheritdoc/>
        public async override Task<AccessToMetrics?> GetByIdAsync(int id)
        {
            return await _contextDb.AccessToMetrics
                .Include(a => a.ProviderUser)
                .Include(a => a.GrantedUser)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <inheritdoc/>
        public async override Task<IEnumerable<AccessToMetrics>> GetAllAsync()
        {
            return await _contextDb.AccessToMetrics
                .Include(a => a.ProviderUser)
                .Include(a => a.GrantedUser)
                .ToListAsync();
        }
    }
}
