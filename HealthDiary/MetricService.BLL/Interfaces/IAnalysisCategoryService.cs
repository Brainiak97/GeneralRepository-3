using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными справочника "Категории анализов"
    /// </summary>
    public interface IAnalysisCategoryService 
    {
        /// <summary>
        /// Создать запись в справочнике
        /// </summary>
        /// <param name="analysisCategoryCreateDTO">Данные для создания записи</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о категории анализов</exception>
        public Task CreateAnalysisCategoryAsync(AnalysisCategoryCreateDTO analysisCategoryCreateDTO);

        /// <summary>
        /// Обновить запись в справочнике
        /// </summary>
        /// <param name="analysisCategoryUpdateDTO">Данные для измения записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Категория анализов не зарегистрирована</exception>       
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о категории анализов</exception>
        public Task UpdateAnalysisCategoryAsync(AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO);

        /// <summary>
        /// Удалить запись в справочнике
        /// </summary>
        /// <param name="analysisCategoryId">Идентификатор записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Категория анализов не зарегистрирована</exception>       
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные</exception>
        public Task DeleteAnalysisCategoryAsync(int analysisCategoryId);


        /// <summary>
        /// Получить запись из справочника
        /// </summary>
        /// <param name="categoryId">Идентификатор записи</param>
        /// <returns>Запись из справочника</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная категория анализов не существует</exception>        
        public Task<AnalysisCategoryDTO?> GetAnalysisCategoryByIdAsync(int categoryId);

        /// <summary>
        /// Получить список записей из справочника
        /// </summary>       
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<AnalysisCategoryDTO>> GetAllAnalysisCategoriesAsync();


        /// <summary>
        /// Получить список записей из справочника, заданной строкой поиска по наименованию и описанию.
        /// В строку поиска можно передать несколько фраз поиска через ","
        /// </summary>
        /// <param name="search">Cтрока поиска по наименованию</param>
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<AnalysisCategoryDTO>> GetListAnalysisCategoriesBySearchAsync(string search);
    }
}
