namespace MetricService.BLL.Exceptions
{
    /// <summary>
    /// Возникает при попытки изменения или удаления сущности, на которую имеются ссылки
    /// </summary>   
    public class ReferenceToEntryException : BaseException
    {
        /// <summary>
        ///  Возникает при попытки изменения или удаления сущности, на которую имеются ссылки
        /// </summary>
        /// <param name="message">Сообщение об ошибки</param>
        /// <param name="entryId">Идентификатор сущности</param>
        /// <param name="referenceCount">Количество ссылок на сущность</param>
        public ReferenceToEntryException(string message, int entryId, int referenceCount) : base(message)
        {
            Data.Add("entryId", entryId);
            Data.Add("referenceCount", referenceCount);            
        }
    }
}
