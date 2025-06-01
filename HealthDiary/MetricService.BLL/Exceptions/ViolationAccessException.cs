namespace MetricService.BLL.Exceptions
{
    internal class ViolationAccessException : ApplicationException
    {
        /// <summary>
        /// Идентификатор инициатора действия
        /// </summary>
        public int AuthorId { get; init; }

        /// <summary>
        /// идентификатор пользователя к данным которого получаем доступ
        /// </summary>
        public int RecordId { get; init; }

        /// <summary>
        /// имя набора данных
        /// </summary>
        public string Entity { get; init; } = null!;

        /// <summary>
        /// возникает при попытки изменения данных без соответствующих прав
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="authorid">Пользователь, от имени которого происходит действие</param>
        /// <param name="recordid">идентификатор изменяемой строки</param>
        /// <param name="entity"></param>
        public ViolationAccessException(string message, int authorid, int recordid, string entity) : base(message)
        {
            AuthorId = authorid;
            RecordId = recordid;
            Entity = entity;
        }

    }
}
