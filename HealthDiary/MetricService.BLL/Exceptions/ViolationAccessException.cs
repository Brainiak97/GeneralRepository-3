﻿namespace MetricService.BLL.Exceptions
{
    /// <summary>
    /// Возникает при попытки изменения данных без соответствующих прав
    /// </summary>
    public class ViolationAccessException : ApplicationException
    {        

        /// <summary>
        /// Возникает при попытки изменения данных без соответствующих прав
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="authorid">Пользователь, от имени которого происходит действие</param>
        /// <param name="recordid">идентификатор изменяемой строки</param>
        /// <param name="entity"></param>
        public ViolationAccessException(string message, int authorid, int recordid, string entity) : base(message)
        {
            Data.Add("authorId", authorid);
            Data.Add("recordId", recordid);
            Data.Add("entity", entity);
        }
    }
}
