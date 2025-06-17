using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    public interface IAnalysisTypeService 
    {
        /// <summary>
        /// Создание типа анализа
        /// </summary>
        /// <param name="analysisTypeCreateDTO">Тип анализа</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные - 0</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о типе анализов</exception>
        public Task CreateAnalysisTypeAsync(AnalysisTypeCreateDTO analysisTypeCreateDTO);

        /// <summary>
        /// Обновление типа анализа
        /// </summary>
        /// <param name="analysisTypeUpdateDTO">Тип анализа</param>
        /// <exception cref="IncorrectOrEmptyResultException">Тип анализов не зарегистрирован</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о типе анализа</exception>
        public Task UpdateAnalysisTypeAsync(AnalysisTypeUpdateDTO analysisTypeUpdateDTO);

        /// <summary>
        /// Удаление типа анализа
        /// </summary>
        /// <param name="analysisTypeId">Тип анализа</param>
        /// <exception cref="IncorrectOrEmptyResultException">Тип анализов не зарегистрирован</exception>        
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные - 0</exception>
        public Task DeleteAnalysisTypeAsync(int analysisTypeId);

        /// <summary>
        /// получение типа анализа по ИД
        /// </summary>
        /// <param name="typeId">ИД типа анализа</param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанный тип анализов не существует</exception>
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
