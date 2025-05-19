# Health diary
Краткое описание проекта

# Зависимости проекта
## Используемые технологии
Технология            | Комментарии
----------------------|-------------
ASP.NET Core          | Убедитесь, что у вас установлен .NET SDK (версия должна соответствовать требованиям проекта).
Entity Framework Core | Для работы с базой данных используется ORM Entity Framework Core.
PostgreSQL            | База данных PostgreSQL должна быть установлена и настроена.

## Установка зависимостей
Для установки всех необходимых библиотек выполните следующие команды:
1. Переход в директорию проекта
```powershell
cd /path/to/your/project
```
2. Восстановление NuGet пакетов
```powershell
dotnet restore
```
3. Если вы используете клиентские библиотеки (например, JavaScript), убедитесь, что они также установлены.
Для этого можно использовать установку клиентских библиотек через `libman`:
```powershell
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
libman restore
```
4. Применение миграций
После настройки строки подключения примените миграции Entity Framework Core:
```powershell
dotnet ef database update
```
5. Запуск приложения
Чтобы запустить проект, выполните следующую команду:
```powershell
dotnet run --project src/HealthDiary/
```
Приложение будет доступно по адресу localhost:port (зависит от настроек appsettings.json).

# Структура проекта
```text
/HealthDiary
  ├── HealthDiary/         # Веб-приложение (UI + API)
  │   ├── Controllers/     	# Контроллеры MVC и API
  │   ├── Views/           	# Представления (Razor Pages)
  │   ├── Models/          	# ViewModel для передачи данных в представления
  │   └── appsettings.json 	# Конфигурация приложения
  ├── Domain/              # Общие данные (Domain Layer)
  │   ├── Interfaces/       # Общие интерфейсы
  │   ├── Models/        	# Общие модели
  ├── BLL/                 # Бизнес-логика (Business Logic Layer)
  │   ├── Dto/             	# Dto модели для передачи данных между уровнями
  │   ├── Services/        	# Сервисы для бизнес-логики
  └── DAL/                 # Доступ к данным (Data Access Layer)
      ├── EF/  			   	# Классы для работы с Entity Framework
      └── Repositories/    	# Реализация паттерна Repository
```

# Настройка базы данных
Прежде чем запустить проект, убедитесь, что база данных PostgreSQL настроена. Строка подключения находится в файле appsettings.json. Проверьте, что параметры подключения корректны:
```json
"ConnectionStrings": {
    "DefaultConnection": "Host=your_host;Database=your_database;Username=your_user;Password=your_password"
}
```
Cтрока подключения описана в файле appsettings.json.

# Веб-интерфейс
Главная страница: /Home/Index
Шаблон маршрутов: "{controller=Home}/{action=Index}/{id?}"

# Архитектура
Проект разделён на три слоя:
Слой 		 | Расшифровка аббривеатуры | Комментарий
-------------|--------------------------|-------------
HealthDiary  | HealthDiary              | Отвечает за взаимодействие с пользователем (MVC, API)
BLL  		 | Business Logic Layer     | Содержит бизнес-логику приложения
DAL  		 | Data Access Layer        | Отвечает за работу с базой данных
Domain		 | Domain Layer		        | Хранит общие данные для всех уровней

# Repository
Все операции с базой данных выполняются через интерфейсы `IRepository<T>` (`Domain/Interfaces`).
Реализация находится в папке `DAL/Repositories/`.
