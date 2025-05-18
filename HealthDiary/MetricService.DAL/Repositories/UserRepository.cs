using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricService.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MetricServiceDbContext _contextDb = null!;

        public UserRepository(MetricServiceDbContext metricServiceDb)
        {
            _contextDb = metricServiceDb;
        }


        public async Task CreateAsync(User item)
        {
            _contextDb.Add<User>(item);

            await _contextDb.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            User user = await _contextDb.Users.FindAsync(id);
            if (user != null)
                _contextDb.Users.Remove(user);

            await _contextDb.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _contextDb.Users.ToListAsync();
        }

        public async Task<User>? GetByIdAsync(int id)
        {
            return await _contextDb.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(User item)
        {
            User user = await GetByIdAsync(item.Id);
            if (user != null)
            {
                user.Weight = item.Weight;
                user.DateOfBirth = item.DateOfBirth;
                user.Height = item.Height;
            }

            await _contextDb.SaveChangesAsync();

        }
    }
}
