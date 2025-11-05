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

        // Настоящие примитивы синхронизации для счётчиков
        private long _doctorCount = 0;
        private long _patientCount = 0;

        private Timer? _cleanupTimer;

        // Вызывается при успешном входе
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
                    default:
                        break;
                }
            }
        }

        // Вызывается при явном logout
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

        // Вызывается при каждом защищённом запросе (middleware)
        public void UpdateActivity(int userId, string role)
        {
            _activeSessions.AddOrUpdate(userId,
                // если новая сессия
                _ => new UserSessionDto(role, DateTime.UtcNow),
                // если существующая — обновляем время
                (_, existing) => new UserSessionDto(existing.Role, DateTime.UtcNow)
            );
        }

        public long GetDoctorCount() => Interlocked.Read(ref _doctorCount);
        public long GetPatientCount() => Interlocked.Read(ref _patientCount);

        // Фоновая очистка каждые 1 минуту
        public Task StartAsync(CancellationToken ct)
        {
            _cleanupTimer = new Timer(CleanupInactiveSessions, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

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
