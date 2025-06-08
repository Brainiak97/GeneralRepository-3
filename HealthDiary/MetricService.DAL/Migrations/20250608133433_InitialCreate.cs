using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetricService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhysicalActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false, comment: "Наименование физической активности"),
                    EnergyEquivalent = table.Column<float>(type: "real", nullable: false, comment: "Метаболический эквивалент")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalActivities", x => x.Id);
                },
                comment: "Тренировки");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор"),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false, comment: "дата рождения"),
                    Height = table.Column<short>(type: "smallint", nullable: false, comment: "рост в сантиметрах"),
                    Weight = table.Column<double>(type: "double precision", nullable: false, comment: "Вес в килограммах")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                },
                comment: "Пользователь");

            migrationBuilder.CreateTable(
                name: "HealthMetricsBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя"),
                    MetricDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата замера показателя"),
                    HeartRate = table.Column<short>(type: "smallint", nullable: false, comment: "Частота сердечных сокращений (ударов/мин)"),
                    BloodPressureSys = table.Column<short>(type: "smallint", nullable: true, comment: "Верхнее артериальное давление (мм рт. ст.)"),
                    BloodPressureDia = table.Column<short>(type: "smallint", nullable: true, comment: "Нижнее артериальное давление (мм рт. ст.)"),
                    BodyFatPercentage = table.Column<float>(type: "real", nullable: true, comment: "Процент жира в организме"),
                    WaterIntake = table.Column<short>(type: "smallint", nullable: true, comment: "Потребление воды (мл)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMetricsBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthMetricsBase_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Базовые медицинские показатели");

            migrationBuilder.CreateTable(
                name: "Sleeps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false, comment: "идентификатор пользователя"),
                    StartSleep = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "время начала сна"),
                    EndSleep = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "время окончания сна"),
                    QualityRating = table.Column<short>(type: "smallint", nullable: false, comment: "качество сна по 5-ой системе"),
                    Comment = table.Column<string>(type: "text", nullable: true, comment: "примечания о качестве сна")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sleeps", x => x.Id);
                    table.CheckConstraint("ValidQualityRating", "\"QualityRating\">=1 and \"QualityRating\"<=5");
                    table.ForeignKey(
                        name: "FK_Sleeps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Сон");

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя"),
                    PhysicalActivityId = table.Column<int>(type: "integer", nullable: false, comment: "Физ. активность"),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Время начала тренировки"),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Время окончания тренировки"),
                    Description = table.Column<string>(type: "text", nullable: true, comment: "Описание")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_PhysicalActivities_PhysicalActivityId",
                        column: x => x.PhysicalActivityId,
                        principalTable: "PhysicalActivities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Workouts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Тренировки");

            migrationBuilder.InsertData(
                table: "PhysicalActivities",
                columns: new[] { "Id", "EnergyEquivalent", "Name" },
                values: new object[,]
                {
                    { 1, 5f, "Аквааэробика (умеренный темп)" },
                    { 2, 5f, "Акваслайдинг (водные горки)" },
                    { 3, 4f, "Активные домашние животные (игра с собакой)" },
                    { 4, 8f, "Альпинизм (взбирание в горы)" },
                    { 5, 4.5f, "Бадминтон" },
                    { 6, 7f, "Бег трусцой (8 км/ч)" },
                    { 7, 10f, "Бокс" },
                    { 8, 8f, "Борьба дзюдо (групповые занятия)" },
                    { 9, 5f, "Быстрая ходьба (более 6 км/ч)" },
                    { 10, 8f, "Быстрое восхождение по лестнице" },
                    { 11, 6f, "Велоспорт (10–16 км/ч)" },
                    { 12, 5.5f, "Верховая езда" },
                    { 13, 5f, "Водная аэробика" },
                    { 14, 4f, "Волейбол" },
                    { 15, 12f, "Высокогорные восхождения" },
                    { 16, 15f, "Высокоинтенсивные интервальные тренировки (HIIT)" },
                    { 17, 6f, "Гимнастика с утяжелителями" },
                    { 18, 12f, "Гонка на выносливость (железный человек)" },
                    { 19, 8f, "Горнолыжный спорт" },
                    { 20, 8f, "Гребля на байдарках или каноэ" },
                    { 21, 3.5f, "Домашние дела (уборка дома)" },
                    { 22, 2f, "Дыхательная гимнастика" },
                    { 23, 3f, "Езда на велосипеде (менее 10 км/ч)" },
                    { 24, 10f, "Забеги на длинные дистанции (марафон)" },
                    { 25, 4f, "Заниматься танцами (танго, вальс)" },
                    { 26, 2.5f, "Занятие йогой (медленные позы)" },
                    { 27, 8f, "Зимние виды спорта (лыжи, сноуборд)" },
                    { 28, 5f, "Игра в теннис (парный режим)" },
                    { 29, 8f, "Игры в баскетбол (не соревновательные)" },
                    { 30, 4.5f, "Йога (силовая практика)" },
                    { 31, 12f, "Кардиотренажёры (интервал-тренировка)" },
                    { 32, 10f, "Классическое плавание (энергичное)" },
                    { 33, 8f, "Командные игры (регби, хоккей)" },
                    { 34, 2.5f, "Концерт (стояние, хоровое исполнение песен)" },
                    { 35, 7f, "Конькобежный спорт" },
                    { 36, 10f, "Кроссфит" },
                    { 37, 3f, "Купание в море (игры, плескание)" },
                    { 38, 7f, "Легкая атлетика (пробежки)" },
                    { 39, 1f, "Лежачий покой" },
                    { 40, 5.5f, "Летний туризм (велосипедные прогулки)" },
                    { 41, 10f, "Марафон" },
                    { 42, 7f, "Обычная игра в футбол (любительский уровень)" },
                    { 43, 6f, "Перемещение тяжелой техники" },
                    { 44, 6f, "Перенос тяжелых предметов (сумки, коробки)" },
                    { 45, 3.5f, "Пилатес" },
                    { 46, 6f, "Плавание (легкий темп)" },
                    { 47, 5f, "Погрузка мебели" },
                    { 48, 3f, "Поездка на мотоцикле" },
                    { 49, 4f, "Покраска стен (ручной труд)" },
                    { 50, 3.5f, "Прогулочный шаг (4–5 км/ч)" },
                    { 51, 1.3f, "Просмотр телевизора" },
                    { 52, 10f, "Профессиональный бег (12 км/ч)" },
                    { 53, 1.5f, "Работа в офисе (сидячая)" },
                    { 54, 4f, "Работа с садовыми инструментами (газонокосилка)" },
                    { 55, 2.3f, "Растяжка" },
                    { 56, 5f, "Ремонт квартиры (перестановка мебели)" },
                    { 57, 3f, "Рыбалка (ловля рыбы удочкой)" },
                    { 58, 4f, "Садоводство (посадка растений, прополка)" },
                    { 59, 8f, "Санный спорт (гонки на санях)" },
                    { 60, 6f, "Сельскохозяйственная работа (ручная обработка земли)" },
                    { 61, 8f, "Серфинг (катание на волнах)" },
                    { 62, 3.5f, "Силовые тренировки (вес менее 5 кг)" },
                    { 63, 8f, "Скалолазание (скалодром)" },
                    { 64, 4.3f, "Скандинавская ходьба" },
                    { 65, 4.5f, "Совместная семейная прогулка (быстрая ходьба)" },
                    { 66, 0.9f, "Сон" },
                    { 67, 13f, "Соревнования по бегу на короткие дистанции" },
                    { 68, 4f, "Спокойная велосипедная поездка (10–16 км/ч)" },
                    { 69, 8f, "Спортивная гимнастика" },
                    { 70, 15f, "Спринт" },
                    { 71, 3.5f, "Стрельба из лука" },
                    { 72, 2.3f, "Стретчинг (растяжка мышц)" },
                    { 73, 4.5f, "Строительство забора" },
                    { 74, 2.5f, "Тай-чи (умеренный темп)" },
                    { 75, 6f, "Танцы (быстрое движение, зумба)" },
                    { 76, 4f, "Танцы (социальные танцы)" },
                    { 77, 6f, "Туризм пеший (горные походы)" },
                    { 78, 5.5f, "Упражнения на велотренажере (умеренная нагрузка)" },
                    { 79, 4f, "Ходьба (5–6 км/ч)" },
                    { 80, 5f, "Хождение пешком (6–7 км/ч)" },
                    { 81, 8f, "Хождение пешком вверх по лестнице" },
                    { 82, 7f, "Чистка снега лопатой" },
                    { 83, 7f, "Эллипсоид (умеренный темп)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetricsBase_UserId",
                table: "HealthMetricsBase",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sleeps_UserId",
                table: "Sleeps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_PhysicalActivityId",
                table: "Workouts",
                column: "PhysicalActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthMetricsBase");

            migrationBuilder.DropTable(
                name: "Sleeps");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "PhysicalActivities");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
