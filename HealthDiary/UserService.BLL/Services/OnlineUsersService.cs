using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using UserService.BLL.Dto;

namespace UserService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для определения количества активных пользователей разных ролей.
    /// </summary>
    public class OnlineUsersService : IHostedService, IDisposable
    {
        private readonly ConcurrentDictionary<int, UserSessionDto> _activeSessions = new();

        private long _doctorCount = 0;
        private long _patientCount = 0;

        private Timer? _cleanupTimer;

        /// <summary>
        /// Вызывается при успешном входе
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="role">Наименвоание назначаемой роли.</param>
        /// <exception cref="ArgumentException"></exception>
        public void RegisterLogin(int userId, string role)
        {
            var session = new UserSessionDto(role, DateTime.UtcNow);
            if (_activeSessions.TryAdd(userId, session))
            {
                // Атомарное увеличение счётчика
                switch (role)
                {
                    case "Doctor":
                        Interlocked.Increment(ref _doctorCount);
                        break;
                    case "User":
                        Interlocked.Increment(ref _patientCount);
                        break;
                    case "Admin":
                        break;
                    default:
                        throw new ArgumentException(role);
                }
            }
        }

        /// <summary>
        /// Вызывается при явном logout
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public void RegisterLogout(int userId)
        {
            if (_activeSessions.TryRemove(userId, out var session))
            {
                // Атомарное уменьшение
                switch (session.Role)
                {
                    case "Doctor":
                        Interlocked.Decrement(ref _doctorCount);
                        break;
                    case "User":
                        Interlocked.Decrement(ref _patientCount);
                        break;
                }
            }
        }

        /// <summary>
        /// Вызывается при каждом защищённом запросе (middleware)
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="role">Наименвоание назначаемой роли.</param>
        public void UpdateActivity(int userId, string role)
        {
            _activeSessions.AddOrUpdate(userId,
                // если новая сессия
                _ => new UserSessionDto(role, DateTime.UtcNow),
                // если существующая — обновляем время
                (_, existing) => new UserSessionDto(existing.Role, DateTime.UtcNow)
            );
        }

        /// <summary>
        /// Возвращает текущее количество врачей в системе.
        /// Значение считывается атомарно с использованием <see cref="Interlocked"/>
        /// для обеспечения потокобезопасности при одновременном доступе из нескольких потоков.
        /// </summary>
        /// <returns>Текущее количество врачей.</returns>
        public long GetDoctorCount() => Interlocked.Read(ref _doctorCount);

        /// <summary>
        /// Возвращает текущее количество пациентов в системе.
        /// Значение считывается атомарно с использованием <see cref="Interlocked"/>
        /// для обеспечения потокобезопасности при одновременном доступе из нескольких потоков.
        /// </summary>
        /// <returns>Текущее количество пациентов.</returns>
        public long GetPatientCount() => Interlocked.Read(ref _patientCount);

        /// <summary>
        /// Фоновая очистка каждые 1 минуту
        /// </summary>
        /// <param name="сancellationToken"></param>
        public Task StartAsync(CancellationToken сancellationToken)
        {
            _cleanupTimer = new Timer(CleanupInactiveSessions, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Декремент счетчиков.
        /// </summary>
        /// <param name="state"></param>
        private void CleanupInactiveSessions(object? state)
        {
            var cutoff = DateTime.UtcNow.AddMinutes(-5); // неактивен >5 мин

            var toRemove = _activeSessions
                .Where(kvp => kvp.Value.LastActivity < cutoff)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var userId in toRemove)
            {
                if (_activeSessions.TryRemove(userId, out var session))
                {
                    // Декремент при удалении неактивного
                    switch (session.Role)
                    {
                        case "Doctor":
                            Interlocked.Decrement(ref _doctorCount);
                            break;
                        case "User":
                            Interlocked.Decrement(ref _patientCount);
                            break;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken ct)
        {
            _cleanupTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void Dispose() => _cleanupTimer?.Dispose();
    }
}
