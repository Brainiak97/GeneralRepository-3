using MetricService.BLL.DTO.AnalysisCategory;

namespace MetricService.BLL.Interfaces
{
    public interface IAnalysisCategoryService 
    {
        public Task CreateAnalysisCategoryAsync(AnalysisCategoryCreateDTO analysisCategoryCreateDTO);

        public Task UpdateAnalysisCategoryAsync(AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO);

        public Task DeleteAnalysisCategoryAsync(int analysisCategoryId);


        /// <summary>
        /// получение категории анализов по ИД
        /// </summary>
        /// <param name="categoryId">ИД категории</param>
        /// <returns></returns>
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
