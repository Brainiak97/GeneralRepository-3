using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    public interface IAnalysisCategoryService 
    {

        /// <summary>
        /// Создание категории анализов
        /// </summary>
        /// <param name="analysisCategoryCreateDTO">Категория анализов</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о категории анализов</exception>
        public Task CreateAnalysisCategoryAsync(AnalysisCategoryCreateDTO analysisCategoryCreateDTO);

        /// <summary>
        /// Обновление категории анализов
        /// </summary>
        /// <param name="analysisCategoryUpdateDTO">Категория анализов</param>
        /// <exception cref="IncorrectOrEmptyResultException">Категория анализов не зарегистрирована</exception>       
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о категории анализов</exception>
        public Task UpdateAnalysisCategoryAsync(AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO);

        /// <summary>
        /// Удаление категории анализов
        /// </summary>
        /// <param name="analysisCategoryId">ИД категории анализов</param>
        /// <exception cref="IncorrectOrEmptyResultException">Категория анализов не зарегистрирована</exception>       
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные - 0</exception>
        public Task DeleteAnalysisCategoryAsync(int analysisCategoryId);


        /// <summary>
        /// получение категории анализов по ИД
        /// </summary>
        /// <param name="categoryId">ИД категории</param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная категория анализов не существует</exception>        
        public Task<AnalysisCategoryDTO?> GetAnalysisCategoryByIdAsync(int categoryId);

        /// <summary>
        /// получение списка категорий анализов
        /// </summary>
        /// <param name="pageNum">номер страницы для пагинации</param>
        /// <param name="pageSize">количество позиций на странице</param>
        /// <returns></returns>
        public Task<IEnumerable<AnalysisCategoryDTO>> GetAllAnalysisCategoriesAsync(int pageNum, int pageSize);


        /// <summary>
        /// получение списка категорий анализов, заданной строкой поиска по наименованию и описанию.
        /// В строку поиска можно передать несколько фраз поиска через ","
        /// </summary>
        /// <param name="search">строка поиска по наименованию</param>
        /// <returns></returns>
        public Task<IEnumerable<AnalysisCategoryDTO>> GetListAnalysisCategoriesBySearchAsync(string search);
    }
}
