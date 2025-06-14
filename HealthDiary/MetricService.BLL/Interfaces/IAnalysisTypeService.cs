using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.DTO.AnalysisType;

namespace MetricService.BLL.Interfaces
{
    public interface IAnalysisTypeService 
    {
        public Task CreateAnalysisTypeAsync(AnalysisTypeCreateDTO analysisTypeCreateDTO);

        public Task UpdateAnalysisTypeAsync(AnalysisTypeUpdateDTO analysisTypeUpdateDTO);

        public Task DeleteAnalysisTypeAsync(int analysisTypeId);

        /// <summary>
        /// получение типа анализа по ИД
        /// </summary>
        /// <param name="typeId">ИД типа анализа</param>
        /// <returns></returns>
        public Task<AnalysisTypeDTO?> GetAnalysisTypeByIdAsync(int typeId);

        /// <summary>
        /// получение списка типа анализов
        /// </summary>
        /// <param name="pageNum">номер страницы для пагинации</param>
        /// <param name="pageSize">количество позиций на странице</param>
        /// <returns></returns>
        public Task<IEnumerable<AnalysisTypeDTO>> GetAllAnalysisTypeAsync(int pageNum, int pageSize);


        /// <summary>
        /// получение списка типов анализов, заданной строкой поиска по наименованию.
        /// В строку поиска можно передать несколько фраз поиска через ","
        /// </summary>
        /// <param name="search">строка поиска по наименованию</param>
        /// <returns></returns>
        public Task<IEnumerable<AnalysisTypeDTO>> GetListAnalysisTypeBySearchAsync(string search);
    }
}
