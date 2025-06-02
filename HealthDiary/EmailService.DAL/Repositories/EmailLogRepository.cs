using EmailService.DAL.EF;
using EmailService.DAL.Interfaces;
using EmailService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailService.DAL.Repositories
{
    /// <summary>
    /// Реализация репозитория для работы с сущностями <see cref="EmailLog"/>.
    /// Предоставляет методы для получения, добавления, обновления и удаления записей о отправленных письмах.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр <see cref="EmailLogRepository"/>.
    /// </remarks>
    /// <param name="context">Контекст базы данных для взаимодействия с таблицей логов.</param>
    public class EmailLogRepository(EmailDbContext context) : IRepository<EmailLog>
    {
        private readonly EmailDbContext _context = context;

        /// <summary>
        /// Асинхронно получает запись лога по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор записи лога.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденную запись или <see langword="null"/>, если запись не найдена.</returns>
        public async Task<EmailLog?> GetByIdAsync(Guid id) =>
            await _context.EmailLogs.FindAsync(id);

        /// <summary>
        /// Асинхронно получает все записи логов из базы данных.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает коллекцию всех записей логов или <see langword="null"/>.</returns>
        public async Task<IEnumerable<EmailLog>?> GetAllAsync() =>
            await _context.EmailLogs.ToListAsync();

        /// <summary>
        /// Асинхронно добавляет новую запись лога в базу данных.
        /// </summary>
        /// <param name="log">Запись лога, которую нужно добавить.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает добавленную запись лога или <see langword="null"/>.</returns>
        public async Task<EmailLog?> AddAsync(EmailLog log)
        {
            await _context.EmailLogs.AddAsync(log);
            await _context.SaveChangesAsync();
            return log;
        }

        /// <summary>
        /// Асинхронно обновляет существующую запись лога в базе данных.
        /// </summary>
        /// <param name="log">Обновлённая запись лога.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает обновлённую запись лога или <see langword="null"/>.</returns>
        public async Task<EmailLog?> UpdateAsync(EmailLog log)
        {
            _context.EmailLogs.Update(log);
            await _context.SaveChangesAsync();
            return log;
        }

        /// <summary>
        /// Асинхронно удаляет указанную запись лога из базы данных.
        /// </summary>
        /// <param name="log">Запись лога, которую нужно удалить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task DeleteAsync(EmailLog log)
        {
            _context.EmailLogs.Remove(log);
            await _context.SaveChangesAsync();
        }
    }
}