using EmailService.DAL.EF;
using EmailService.DAL.Interfaces;
using EmailService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailService.DAL.Repositories
{
    /// <summary>
    /// Реализация репозитория для работы с сущностями <see cref="EmailTemplate"/>.
    /// Предоставляет методы для получения, добавления, обновления и удаления шаблонов электронных писем.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр <see cref="EmailTemplateRepository"/>.
    /// </remarks>
    /// <param name="context">Контекст базы данных для взаимодействия с таблицей шаблонов.</param>
    public class EmailTemplateRepository(EmailDbContext context) : IRepository<EmailTemplate>
    {
        private readonly EmailDbContext _context = context;

        /// <summary>
        /// Асинхронно получает шаблон по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор шаблона.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденный шаблон или <see langword="null"/>, если шаблон не найден.</returns>
        public async Task<EmailTemplate?> GetByIdAsync(Guid id) =>
            await _context.EmailTemplates.FindAsync(id);

        /// <summary>
        /// Асинхронно получает все шаблоны из базы данных.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает коллекцию всех шаблонов или <see langword="null"/>.</returns>
        public async Task<IEnumerable<EmailTemplate>?> GetAllAsync() =>
            await _context.EmailTemplates.ToListAsync();

        /// <summary>
        /// Асинхронно добавляет новый шаблон в базу данных.
        /// </summary>
        /// <param name="template">Шаблон, который нужно добавить.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает добавленный шаблон или <see langword="null"/>.</returns>
        public async Task<EmailTemplate?> AddAsync(EmailTemplate template)
        {
            await _context.AddAsync(template);
            await _context.SaveChangesAsync();
            return template;
        }

        /// <summary>
        /// Асинхронно обновляет существующий шаблон в базе данных.
        /// </summary>
        /// <param name="template">Обновлённый шаблон.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает обновлённый шаблон или <see langword="null"/>.</returns>
        public async Task<EmailTemplate?> UpdateAsync(EmailTemplate template)
        {
            _context.EmailTemplates.Update(template);
            await _context.SaveChangesAsync();
            return template;
        }

        /// <summary>
        /// Асинхронно удаляет указанный шаблон из базы данных.
        /// </summary>
        /// <param name="template">Шаблон, который нужно удалить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task DeleteAsync(EmailTemplate template)
        {
            _context.EmailTemplates.Remove(template);
            await _context.SaveChangesAsync();
        }
    }
}