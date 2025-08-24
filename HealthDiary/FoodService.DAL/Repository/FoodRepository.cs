using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Interfaces;
using Team3.HealthDiary.Shared.Common.Exceptions;

namespace Team3.HealthDiary.FoodService.DAL.Repository
{
	public class FoodRepository : IFoodRepository
	{
		private readonly FoodServiceDbContext _dbContext;
		private readonly IMapper _mapper;

		public FoodRepository( FoodServiceDbContext dbContext, IMapper mapper )
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<T?> GetByIdAsync<T, TKey>( TKey entryId ) where T : class, IEntityModel<TKey>
		{
			var entry = await _dbContext.Set<T>().SingleOrDefaultAsync( x => x.Id!.Equals( entryId ) );
			return entry;
		}

		public async Task<List<T>> GetAll<T>( Expression<Func<T, bool>>? condition = null ) where T : class
		{
			var enties = await _dbContext.Set<T>()
				.Where( condition ?? ( x => true ) )
				.ToListAsync();
			return enties;
		}

		public async Task<T> AddAsync<T>( T newEntry ) where T : class
		{
			var entry = await _dbContext.AddAsync( newEntry );
			await _dbContext.SaveChangesAsync();
			return entry.Entity;
		}

		public async Task UpdateAsync<T, TKey>( T entryNew ) where T : class, IEntityModel<TKey>
		{
			var entryCurrent = await _dbContext.FindAsync<T>( entryNew.Id );
			if ( entryCurrent is null )
			{
				throw new EntryNotFoundException( $"Запись {entryNew.GetType()}{entryNew} не найдена для обновления" );
			}

			_mapper.Map( entryNew, entryCurrent );

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync<T, TKey>( TKey entryId ) where T : class, IEntityModel<TKey>
		{
			var entry = await _dbContext.FindAsync<T>( entryId );
			if ( entry == null )
			{
				throw new EntryNotFoundException( $"Запись {typeof( T )} по ключу {entryId} не найдена для удаления" );
			}

			_dbContext.Remove( entry );
			await _dbContext.SaveChangesAsync();
		}
	}
}
