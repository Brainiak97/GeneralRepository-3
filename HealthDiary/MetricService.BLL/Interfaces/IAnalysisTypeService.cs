using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными справочника "Типы анализов"
    /// </summary>
    public interface IAnalysisTypeService 
    {
        /// <summary>
        /// Создать запись в справочнике
        /// </summary>
        /// <param name="analysisTypeCreateDTO">Данные для создания записи</param>
        /// <exception cref="ViolationAccessException">Вы не можете создавать данные</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о типе анализов</exception>
        public Task CreateAnalysisTypeAsync(AnalysisTypeCreateDTO analysisTypeCreateDTO);

        /// <summary>
        /// Изменить запись в справочнике
        /// </summary>
        /// <param name="analysisTypeUpdateDTO">Данные для изменения записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Тип анализов не зарегистрирован</exception>        
        /// <exception cref="ViolationAccessException">Вы не можете изменять данные</exception>
        /// <exception cref="ValidateModelException">Некорректные данные о типе анализа</exception>
        public Task UpdateAnalysisTypeAsync(AnalysisTypeUpdateDTO analysisTypeUpdateDTO);

        /// <summary>
        /// Удалить запись из справочника
        /// </summary>
        /// <param name="analysisTypeId">Идентификатор записи</param>
        /// <exception cref="IncorrectOrEmptyResultException">Тип анализов не зарегистрирован</exception>        
        /// <exception cref="ViolationAccessException">Вам не разрешено удалить данные - 0</exception>
        public Task DeleteAnalysisTypeAsync(int analysisTypeId);

        /// <summary>
        /// Получить запись из справочника
        /// </summary>
        /// <param name="typeId">Идентификатор записи</param>
        /// <returns>Запись из справочника</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанный тип анализов не существует</exception>
        public Task<AnalysisTypeDTO?> GetAnalysisTypeByIdAsync(int typeId);

        /// <summary>
        /// Получить список записей из справочника
        /// </summary>        
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<AnalysisTypeDTO>> GetAllAnalysisTypeAsync();


        /// <summary>
        /// Получение списка записей из справочника, заданной строкой поиска по наименованию.
        /// В строку поиска можно передать несколько фраз поиска через ","
        /// </summary>
        /// <param name="search">Строка поиска по наименованию</param>
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<AnalysisTypeDTO>> GetListAnalysisTypeBySearchAsync(string search);
    }
}
