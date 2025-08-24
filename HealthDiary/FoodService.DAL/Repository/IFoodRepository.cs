using System.Linq.Expressions;
using Shared.Common.Interfaces;
using Team3.HealthDiary.Shared.Common.Exceptions;

namespace Team3.HealthDiary.FoodService.DAL.Repository
{
	public interface IFoodRepository
	{
		/// <summary>
		/// Возвращает объект с указанным Id
		/// </summary>
		/// <typeparam name="T">Тип объекта</typeparam>
		/// <typeparam name="TKey">Тип Id</typeparam>
		/// <param name="entryId">Значение Id</param>
		/// <returns></returns>
		Task<T?> GetByIdAsync<T, TKey>( TKey entryId ) where T : class, IEntityModel<TKey>;

		/// <summary>
		/// Возвращает все объекты удовлетворяющие условию
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="condition">Условие фильтрации</param>
		/// <returns></returns>
		Task<List<T>> GetAll<T>( Expression<Func<T, bool>>? condition = null ) where T : class;

		/// <summary>
		/// Добавляет новый объект - создаётся объект с такими же значениями полей и новым Id
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="newEntry">Объект, из которого беруться значения всех полей кроме Id</param>
		/// <returns>Добавленный объект</returns>
		Task<T> AddAsync<T>( T newEntry ) where T : class;

		/// <summary>
		/// Обновляет поля объекта с таким же Id
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="entry">Объект с новыми значениями полей</param>
		/// <returns></returns>
		/// <exception cref="EntryNotFoundException">Возникает, если не найден объект с таким же Id</exception>
		Task UpdateAsync<T, TKey>( T entry ) where T : class, IEntityModel<TKey>;

		/// <summary>
		/// Удаляет объект по Id
		/// </summary>
		/// <typeparam name="T">Тип объекта</typeparam>
		/// <typeparam name="TKey">Тип Id</typeparam>
		/// <param name="entryId">Значение Id</param>
		/// <returns></returns>
		Task DeleteAsync<T, TKey>( TKey entryId ) where T : class, IEntityModel<TKey>;
	}
}
