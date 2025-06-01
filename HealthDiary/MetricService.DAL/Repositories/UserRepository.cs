using MetricService.DAL.EF;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.DAL.Repositories
{
    public class UserRepository : WriteBaseRepository<User>, IUserRepository
    {
        public UserRepository(MetricServiceDbContext metricServiceDb) : base(metricServiceDb) { }

        public override async Task<bool> CreateAsync(User item)
        {                    
            _contextDb.Add(item);
            return await _contextDb.SaveChangesAsync() == 1;
        }

        public override async Task<bool> UpdateAsync(User item)
        {
            User? user = await GetByIdAsync(item.Id);
            if (user != null)
            {
                user.Weight = item.Weight;
                user.DateOfBirth = item.DateOfBirth;
                user.Height = item.Height;
            }

            return await _contextDb.SaveChangesAsync() == 1;
        }
    }
}
