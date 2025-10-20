using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с данными о профиле пользователя
    /// </summary>
    /// <seealso cref="BaseRepository&lt;User&gt;" />
    /// <seealso cref="IUserRepository" />
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// Cоздать новый объект репозитория<see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="metricServiceDb">Контекст базы данных MetricService</param>
        public UserRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb) { }

        /// <inheritdoc/>
        public override async Task<bool> UpdateAsync(User item)
        {
            User? user = await GetByIdAsync(item.Id);
            if (user != null)
            {
                user.Weight = item.Weight;
                user.Height = item.Height;
            }
            return await _contextDb.SaveChangesAsync() == 1;
        }
    }
}
