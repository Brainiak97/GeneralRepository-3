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
                name: "AnalysisCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false, comment: "Наименование категории анализа"),
                    Description = table.Column<string>(type: "text", nullable: true, comment: "Описание категории анализа")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisCategories", x => x.Id);
                },
                comment: "Категории анализов");

            migrationBuilder.CreateTable(
                name: "DosageForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false, comment: "Наименование формы выпуска (таблетка, капсул, раствор и т.д.)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosageForms", x => x.Id);
                },
                comment: "Форма выпуска препарата");

            migrationBuilder.CreateTable(
                name: "HealthMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false, comment: "Наименование показателя (например, \"Уровень стресса\")"),
                    Description = table.Column<string>(type: "text", nullable: true, comment: "Описание показателя (например, \"Оценивайте стресс от 1 до 10\")"),
                    Unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "Единица измерения (кг., мм.рт.ст., % и т.д.)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMetrics", x => x.Id);
                },
                comment: "Показатель здоровья пользователя");

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
                comment: "Физическая активность");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false, comment: "дата рождения"),
                    Height = table.Column<short>(type: "smallint", nullable: false, comment: "рост в сантиметрах"),
                    Weight = table.Column<float>(type: "real", nullable: false, comment: "Вес в килограммах")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                },
                comment: "Пользователь");

            migrationBuilder.CreateTable(
                name: "AnalysisTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnalysisCategoryId = table.Column<int>(type: "integer", nullable: false, comment: "Ссылка на категорию анализа"),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false, comment: "Название конкретного анализа(например, «Лейкоциты», «Холестерин»)"),
                    ReferenceValueMale = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true, comment: "Эталонное значение мужской"),
                    ReferenceValueFemale = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true, comment: "Эталонное значение женский"),
                    Unit = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisTypes_AnalysisCategories_AnalysisCategoryId",
                        column: x => x.AnalysisCategoryId,
                        principalTable: "AnalysisCategories",
                        principalColumn: "Id");
                },
                comment: "Типы анализов");

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false, comment: "Наименование препарата"),
                    DosageFormId = table.Column<int>(type: "integer", nullable: false, comment: "Форма выпуска (таблетка, капсул, раствор и т.д.)"),
                    Instruction = table.Column<string>(type: "text", nullable: false, comment: "Инструкции по применению")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_DosageForms_DosageFormId",
                        column: x => x.DosageFormId,
                        principalTable: "DosageForms",
                        principalColumn: "Id");
                },
                comment: "Медикаменты");

            migrationBuilder.CreateTable(
                name: "AccessToMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProviderUserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя, который предоставляет доступ"),
                    GrantedUserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя, которому предоставляется доступ"),
                    AccessExpirationDate = table.Column<DateOnly>(type: "DATE", nullable: true, comment: "Дата, до которой действует доступ"),
                    IsPermanentAccess = table.Column<bool>(type: "boolean", nullable: false, comment: "Доступ без ограничения по срокам")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessToMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessToMetrics_Users_GrantedUserId",
                        column: x => x.GrantedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessToMetrics_Users_ProviderUserId",
                        column: x => x.ProviderUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Доступ к личным метрикам");

            migrationBuilder.CreateTable(
                name: "HealthMetricValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя"),
                    HealthMetricId = table.Column<int>(type: "integer", nullable: false, comment: "Ссылка на созданный пользователем показатель"),
                    Value = table.Column<float>(type: "real", nullable: false, comment: "Значение показателя"),
                    RecordedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата и время записи"),
                    Comment = table.Column<string>(type: "text", nullable: true, comment: "Комментарий к записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMetricValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthMetricValues_HealthMetrics_HealthMetricId",
                        column: x => x.HealthMetricId,
                        principalTable: "HealthMetrics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HealthMetricValues_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Значение показателя здоровья пользователя");

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

            migrationBuilder.CreateTable(
                name: "AnalysisResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя"),
                    AnalysisTypeId = table.Column<int>(type: "integer", nullable: false, comment: "Тип анализа"),
                    Value = table.Column<float>(type: "real", nullable: true, comment: "Числовое значение результата анализа"),
                    DetailedResearchDescription = table.Column<string>(type: "text", nullable: true, comment: "Развернутое описание исследования"),
                    TestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата сдачи анализа"),
                    Comment = table.Column<string>(type: "text", nullable: true, comment: "Любые заметки или замечания по этому анализу")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisResults_AnalysisTypes_AnalysisTypeId",
                        column: x => x.AnalysisTypeId,
                        principalTable: "AnalysisTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnalysisResults_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Результаты анализов");

            migrationBuilder.CreateTable(
                name: "Regimens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false, comment: "Пользователь"),
                    MedicationId = table.Column<int>(type: "integer", nullable: false, comment: "Медицинский препарат"),
                    Dosage = table.Column<string>(type: "text", nullable: false, comment: "Прописанная дозировка (например, \"1 табл.\" или \"5 мл\")"),
                    Shedule = table.Column<string>(type: "text", nullable: false, comment: "График приема (например, \"Утро, обед, вечер\")"),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false, comment: "Дата начала приема"),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true, comment: "Предполагаемая дата окончания приема"),
                    Comment = table.Column<string>(type: "text", nullable: true, comment: "Заметки или дополнения")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regimens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regimens_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Regimens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Схема приема медикаментов");

            migrationBuilder.CreateTable(
                name: "Intakes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegimenId = table.Column<int>(type: "integer", nullable: false, comment: "Схема приема лекарств"),
                    TakenAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата и время приема"),
                    IntakeStatus = table.Column<short>(type: "smallint", nullable: false, comment: "Статусы приема (например, \"принято\", \"пропущено\", \"перенесено\")"),
                    Comment = table.Column<string>(type: "text", nullable: true, comment: "Дополнительные заметки (например, причины пропуска)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intakes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intakes_Regimens_RegimenId",
                        column: x => x.RegimenId,
                        principalTable: "Regimens",
                        principalColumn: "Id");
                },
                comment: "Прием лекарств");

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegimenId = table.Column<int>(type: "integer", nullable: false, comment: "Схема приема лекарств"),
                    RemindAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Время напоминания"),
                    IsSend = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак, было ли отправлено напоминание")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_Regimens_RegimenId",
                        column: x => x.RegimenId,
                        principalTable: "Regimens",
                        principalColumn: "Id");
                },
                comment: "Напоминание о приеме лекарств");

            migrationBuilder.InsertData(
                table: "AnalysisCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Стандартные анализы крови, мочи, слюны, фекалий", "Общеклинические исследования" },
                    { 2, "Анализы, определяющие состав и метаболизм веществ в теле", "Биохимические исследования" },
                    { 3, "Анализы гормонов и их регуляторов", "Гормональные исследования" },
                    { 4, "Определение наличия антител и белков иммунной защиты", "Серологические исследования" },
                    { 5, "Анализы бактериального происхождения", "Бактериологические исследования" },
                    { 6, "Исследования мужской половой сферы", "Андрогенно-половые исследования" },
                    { 7, "Анализ генетического материала (ДНК, хромосомы)", "Генетические исследования" },
                    { 8, "Анализы женской репродуктивной системы", "Гинекологические исследования" },
                    { 9, "Анализы на наличие паразитарных организмов", "Паразитологические исследования" },
                    { 10, "Анализы на туберкулез", "Туберкулёзные исследования" },
                    { 11, "Анализ тканей организма", "Гистологические исследования" },
                    { 12, "Аппаратные исследования органов и систем", "Функциональные исследования" },
                    { 13, "Анализы иммунной системы", "Иммунологические исследования" },
                    { 14, "Анализы ревматологических заболеваний", "Ревматологические исследования" },
                    { 15, "Анализы нарушений эндокринной системы", "Эндокринологические исследования" },
                    { 16, "Женские гормоны и менструация", "Гинекологическо-эндокринные исследования" },
                    { 17, "Анализы мочевыделительной системы", "Урологические исследования" },
                    { 18, "Анализы патологий крови и сосудов", "Заболевания кровеносной системы" },
                    { 19, "Баланс электролитов и газов в крови", "Нарушения кислотно-щелочного равновесия" },
                    { 20, "Анализы секреторной функции поджелудочной железы", "Заболевания поджелудочной железы" },
                    { 21, "Анализы диабета и преддиабета", "Заболевания углеводного обмена" },
                    { 22, "Анализы желудочно-кишечного тракта", "Заболевания ЖКТ" },
                    { 23, "Диагностика психических расстройств", "Психиатрические исследования" },
                    { 24, "Анализы аллергических реакций", "Аллергические исследования" },
                    { 25, "Анализы уровня гликированного гемоглобина", "Гликозилирующие исследования" },
                    { 26, "Анализ наследственных факторов риска заболеваний", "Генетически-наследственные заболевания" },
                    { 27, "Анализы сердечной деятельности", "Заболевания сердечно-сосудистой системы" },
                    { 28, "Биомаркеры скрытых воспалительных процессов", "Латентные воспалительные реакции" },
                    { 29, "Анализы зрительного аппарата", "Офтальмологические исследования" },
                    { 30, "Анализы нервной системы", "Неврологические исследования" },
                    { 31, "Анализ цереброспинальной, плевральной жидкости", "Образцы жидкостей организма" },
                    { 32, "Исследования внутренних органов с эндоскопом", "Эндоскопические исследования" },
                    { 33, "Постоянный контроль жизненно важных функций", "Суточные мониторинги" },
                    { 34, "Анализ дыхательных путей и лёгких", "Дыхательная система" },
                    { 35, "Анализы беременности и родоразрешения", "Беременность и роды" },
                    { 36, "Анализы воспалений суставов, мягких тканей", "Воспалительные заболевания" },
                    { 37, "Анализы глаза и офтальмологического статуса", "Орган зрения" },
                    { 38, "Анализы прямой кишки и ануса", "Проктологические исследования" },
                    { 39, "Анализ сердечного ритма и нарушений", "Диагностика аритмий" },
                    { 40, "Анализ бронхиальной проходимости и лёгочной вентиляции", "Бронхолегочная патология" },
                    { 41, "Анализы почечной недостаточности и нефропатий", "Нефротические синдромы" },
                    { 42, "Анализ водно-электролитного баланса", "Водно-электролитный баланс" },
                    { 43, "Анализ пищевода и желудка", "Пищеводно-кишечные нарушения" },
                    { 44, "Анализ костно-мышечной системы", "Травматология и ортопедия" },
                    { 45, "Анализ неврологической природы приступов", "Эпилепсия и судорожные расстройства" },
                    { 46, "Томографические исследования организма", "Магнитно-резонансная томография" },
                    { 47, "Анализ мозговых волн и нервных импульсов", "Функциональная нервная деятельность" }
                });

            migrationBuilder.InsertData(
                table: "DosageForms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "таблетки" },
                    { 2, "капсулы" },
                    { 3, "сироп" },
                    { 4, "раствор" },
                    { 5, "капли" },
                    { 6, "мазь" },
                    { 7, "ампулы" }
                });

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

            migrationBuilder.InsertData(
                table: "AnalysisTypes",
                columns: new[] { "Id", "AnalysisCategoryId", "Name", "ReferenceValueFemale", "ReferenceValueMale", "Unit" },
                values: new object[,]
                {
                    { 1, 2, "Аланинаминотрансфераза (АЛТ)", "До 34", "До 41", "Ед/л" },
                    { 2, 17, "Альдостерон", "2—16", "2—16", "нг/дл" },
                    { 3, 39, "Амбулаторная холтеровская регистрация ЭКГ", "Норма", "Норма", "" },
                    { 4, 23, "Анализ венозной крови на токсины и наркотики", "Нет", "Нет", "Да/Нет" },
                    { 5, 6, "Анализ на вирус гриппа", "Нет", "Нет", "Да/Нет" },
                    { 6, 37, "Анализ спинномозговой жидкости (СМЖ)", "Норма", "Норма", "" },
                    { 7, 4, "Антитела IgG к коронавирусу SARS-CoV-2", "Нет", "Нет", "Да/Нет" },
                    { 8, 2, "Аспаратаминотрансфераза (АСТ)", "До 31", "До 37", "Ед/л" },
                    { 9, 5, "Бакпосев мочи", "Отсутствие роста", "Отсутствие роста", "Колонии/мл" },
                    { 10, 5, "Бактерии кишечной флоры (микробиология стула)", "Норма микрофлоры", "Норма микрофлоры", "кол-во кл/мл" },
                    { 11, 34, "Беременность (анализ на ХГЧ)", "Положительная (беремен.)", "Не применимо", "мМЕ/мл" },
                    { 12, 30, "Биохимический анализ мочи", "Норма", "Норма", "" },
                    { 13, 2, "Биохимия крови", "—", "—", "—" },
                    { 14, 4, "ВИЧ-антиген/антитело", "Нет", "Нет", "Да/Нет" },
                    { 15, 28, "Волчаночный антикоагулянт (ВА)", "Нет", "Нет", "Да/Нет" },
                    { 16, 35, "Воспалительные процессы (С-реактивный белок)", "Менее 5", "Менее 5", "мг/л" },
                    { 17, 9, "Гельминтозы (яйца глистов)", "Нет яиц", "Нет яиц", "" },
                    { 18, 1, "Гемоглобин", "120—150", "130—170", "г/л" },
                    { 19, 11, "Гистология опухоли", "Доброкачественный", "Доброкачественный", "—" },
                    { 20, 25, "Гликозилированный гемоглобин (HbA1c)", "Менее 6,5%", "Менее 6,5%", "%" },
                    { 21, 2, "Глюкоза", "3,9—6,1", "3,9—6,1", "ммоль/л" },
                    { 22, 3, "Гормональные исследования", "", "", "" },
                    { 23, 10, "Диаскинтест (для туберкулеза)", "Нет", "Нет", "Да/Нет" },
                    { 24, 7, "ДНК-исследование BRCA1/BRCA2", "Нет", "Нет", "Да/Нет" },
                    { 25, 20, "Железо сывороточное", "9,0—30,4", "11,6—31,3", "мкмоль/л" },
                    { 26, 13, "Иммуноглобулин E (IgE)", "Менее 100", "Менее 100", "кЕд/л" },
                    { 27, 31, "Иммунограммой", "Норма", "Норма", "" },
                    { 28, 6, "Исследование мокроты", "—", "—", "—" },
                    { 29, 42, "Кислотно-основное состояние (КОС)", "Норма", "Норма", "" },
                    { 30, 19, "Коагулограмма (Международное нормализованное отношение INR)", "0,8—1,2", "0,8—1,2", "Ед" },
                    { 31, 1, "Количество лейкоцитов", "4—10", "4—10", "×10⁹/л" },
                    { 32, 38, "Колоноскопия кишечника", "Норма", "Норма", "" },
                    { 33, 44, "Компьютерная томография головы", "Норма", "Норма", "" },
                    { 34, 20, "Кровеносные газы (pCO₂, pO₂)", "35—45 (pCO₂)/80—100(pO₂)", "35—45 (pCO₂)/80—100(pO₂)", "мм рт. ст." },
                    { 35, 1, "Лейкоциты", "4—10", "4—10", "×10⁹/л" },
                    { 36, 21, "Магний", "0,7—1,1", "0,7—1,1", "ммоль/л" },
                    { 37, 8, "Мазок из влагалища (цитология)", "Без атипичных клеток", "Без атипичных клеток", "" },
                    { 38, 33, "Межпозвонковая грыжа (МРТ поясничного отдела позвоночника)", "Норма", "Норма", "" },
                    { 39, 8, "Микроскопия мазка", "Норма", "Норма", "—" },
                    { 40, 7, "Молекулярно-генетические", "—", "—", "—" },
                    { 41, 2, "Мочевина", "2,5—7,1", "2,8—8,3", "ммоль/л" },
                    { 42, 46, "МРТ головного мозга", "Норма", "Норма", "" },
                    { 43, 7, "Мутация BRCA1/BRCA2", "Отсутствует мутация", "Отсутствует мутация", "Да/Нет" },
                    { 44, 32, "Наркотики и психоактивные вещества (алкоголь, никотин)", "Нет", "Нет", "" },
                    { 45, 30, "Новорождённому скрининг", "Согласно нормативам ВОЗ", "Согласно нормативам ВОЗ", "—" },
                    { 46, 1, "Общий анализ крови", "", "", "" },
                    { 47, 41, "Общий анализ мочи", "Норма", "Норма", "" },
                    { 48, 19, "Онкомаркер ПСА", "Не применяется", "Менее 4,0", "нг/мл" },
                    { 49, 36, "Осмотр глазного дна (офтальмоскопия)", "Норма", "Норма", "" },
                    { 50, 24, "Панель аллергенов", "Норма", "Норма", "" },
                    { 51, 26, "Полиморфизм генов предрасположенности к заболеваниям", "Норма", "Норма", "" },
                    { 52, 16, "Прогестерон (лютеиновая фаза)", "1,5—56,6 (2-я фаза цикла)", "Менее 5,3", "нмоль/л" },
                    { 53, 3, "Пролактин", "4,5—25", "2,5—17", "нг/мл" },
                    { 54, 14, "Ревматоидный фактор (РФ)", "Менее 14", "Менее 14", "Ед/л" },
                    { 55, 25, "С-реактивный белок", "Менее 5", "Менее 5", "мг/л" },
                    { 56, 3, "Свободный тироксин (FT4)", "12—22", "12—22", "пмоль/л" },
                    { 57, 4, "Серология", "", "", "" },
                    { 58, 1, "Скорость оседания эритроцитов (СОЭ)", "2—15", "1—10", "мм/ч" },
                    { 59, 29, "СКРИНИНГ новорожденных", "Соответствие стандартам", "Соответствие стандартам", "" },
                    { 60, 1, "СОЭ (скорость оседания)", "2—15", "1—10", "мм/ч" },
                    { 61, 6, "Спермограмма", "Не применимо", "Нормы согласно ВОЗ", "" },
                    { 62, 21, "Тест толерантности к глюкозе (ГТТ)", "Менее 7,8 спустя 2 ч", "Менее 7,8 спустя 2 ч", "ммоль/л" },
                    { 63, 3, "Тиреотропный гормон (ТТГ)", "0,4—4,0", "0,4—4,0", "мЕд/л" },
                    { 64, 3, "Тироксин свободный (FT₄)", "12—22", "12—22", "пмоль/л" },
                    { 65, 43, "Толстый кишечник (эндоскопия толстой кишки)", "Норма", "Норма", "" },
                    { 66, 15, "Трийодтиронин (Т3)", "1,2—2,8", "1,2—2,8", "нмоль/л" },
                    { 67, 1, "Тромбоциты", "150—400", "150—400", "×10⁹/л" },
                    { 68, 18, "Уровень железа в сыворотке крови", "9,0—30,4", "11,6—31,3", "мкмоль/л" },
                    { 69, 23, "Фолиевая кислота", "3—17", "3—17", "нг/мл" },
                    { 70, 15, "Фолликулостимулирующий гормон (ФСГ)", "1,7—25,0 (1-я фаза цикла)", "1,5—12,4 (1-я фаза цикла)", "мЕд/л" },
                    { 71, 40, "Функциональная диагностика дыхания (спирометрия)", "Норма", "Норма", "" },
                    { 72, 2, "Холестерин общий", "Менее 5,2", "Менее 5,2", "ммоль/л" },
                    { 73, 22, "Цервикальный соскоб на цитологию", "Без признаков дисплазии", "Без признаков дисплазии", "" },
                    { 74, 10, "Цитология шейки матки", "Без атипичных клеток", "Без атипичных клеток", "" },
                    { 75, 22, "Щёлочная фосфатаза", "35—105", "40—150", "Ед/л" },
                    { 76, 12, "Электрокардиограмма (ЭКГ)", "Норма", "Норма", "" },
                    { 77, 45, "Электроэнцефалография (ЭЭГ)", "Норма", "Норма", "" },
                    { 78, 1, "Эритроциты", "3,7—5,0", "4,0—5,5", "×10¹²/л" },
                    { 79, 9, "Яйца гельминтов", "Нет яиц", "Нет яиц", "" },
                    { 80, 4, "Anti-HBs (маркер иммунитета к гепатиту B)", "Да", "Да", "Да/Нет" },
                    { 81, 29, "BRCA-маркеры рака груди", "Отсутствуют мутации", "Отсутствуют мутации", "Да/Нет" },
                    { 82, 24, "D-димер", "Менее 243", "Менее 243", "нг/мл" },
                    { 83, 4, "RW (сифилис)", "Нет", "Нет", "Да/Нет" }
                });

            migrationBuilder.InsertData(
                table: "Medications",
                columns: new[] { "Id", "DosageFormId", "Instruction", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Взрослым и детям старше 12 лет по 1 таблетке каждые 4-6 часов, максимум 4 грамма в сутки.", "Парацетамол" },
                    { 2, 2, "Взрослым по 1 капсуле 3-4 раза в день, максимальная доза 2400 мг/сутки.", "Ибупрофен" },
                    { 3, 2, "Одна капсула утром перед едой, 1 раз в сутки. Курс длится от 1 месяца до полугода.", "Омепразол" },
                    { 4, 1, "По 1 таблетке 3 раза в день, максимальный курс приема — месяц.", "Фенибут" },
                    { 5, 3, "Взрослым — 15-45 мл сиропа однократно вечером, до достижения регулярного эффекта.", "Дюфалак" },
                    { 6, 4, "Раствор внутримышечно по 2-4 мл ежедневно в течение двух недель.", "Актовегин" },
                    { 7, 1, "По 1-2 таблетки рассасывать трижды в день, курс — неделя.", "Лизобакт" },
                    { 8, 1, "Одну таблетку 1-3 раза в сутки взрослым и подросткам старше 14 лет.", "Супрастин" },
                    { 9, 2, "Один раз в сутки, начиная с дозы 150 мг. Лечение кандидоза продолжается до исчезновения симптомов.", "Флуконазол" },
                    { 10, 1, "Начальная доза составляет 2 таблетки в день, постепенно увеличиваясь до 4 таблеток.", "Гептрал" },
                    { 11, 5, "Развести 30 капель в небольшом количестве воды, пить 3 раза в день.", "Новопассит" },
                    { 12, 5, "15-20 капель внутрь до еды, разводят водой, принимают 3 раза в день.", "Валокордин" },
                    { 13, 1, "По 1-2 таблетки во время еды, в зависимости от тяжести пищеварения.", "Мезим форте" },
                    { 14, 1, "Обычно принимается по 1 таблетке 3 раза в день в течение 2-4 недель.", "Афобазол" },
                    { 15, 2, "Капсулы принимаются внутрь во время еды, от 1 до 3 штук за раз.", "Креон" },
                    { 16, 6, "Наносить тонким слоем на поражённое место 2-3 раза в день.", "Диклофенак" },
                    { 17, 1, "Начинают с минимальной дозы 1 таблетка в день, повышая дозу до максимальной в течение месяца.", "Артра" },
                    { 18, 1, "По 1 таблетке под язык 2-3 раза в день. Максимальный курс приема — месяц.", "Глицин" },
                    { 19, 1, "Детям старше 12 лет и взрослым назначают по 1 таблетке 3-4 раза в сутки.", "Левомицетин" },
                    { 20, 7, "Раствор вводят внутривенно медленно, один раз в сутки, на протяжении 10-14 дней.", "Милдронат" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessToMetrics_GrantedUserId",
                table: "AccessToMetrics",
                column: "GrantedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessToMetrics_ProviderUserId",
                table: "AccessToMetrics",
                column: "ProviderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisResults_AnalysisTypeId",
                table: "AnalysisResults",
                column: "AnalysisTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisResults_UserId",
                table: "AnalysisResults",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisTypes_AnalysisCategoryId",
                table: "AnalysisTypes",
                column: "AnalysisCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetricValues_HealthMetricId",
                table: "HealthMetricValues",
                column: "HealthMetricId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetricValues_UserId",
                table: "HealthMetricValues",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_RegimenId",
                table: "Intakes",
                column: "RegimenId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DosageFormId",
                table: "Medications",
                column: "DosageFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Regimens_MedicationId",
                table: "Regimens",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Regimens_UserId",
                table: "Regimens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_RegimenId",
                table: "Reminders",
                column: "RegimenId");

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
                name: "AccessToMetrics");

            migrationBuilder.DropTable(
                name: "AnalysisResults");

            migrationBuilder.DropTable(
                name: "HealthMetricValues");

            migrationBuilder.DropTable(
                name: "Intakes");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Sleeps");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "AnalysisTypes");

            migrationBuilder.DropTable(
                name: "HealthMetrics");

            migrationBuilder.DropTable(
                name: "Regimens");

            migrationBuilder.DropTable(
                name: "PhysicalActivities");

            migrationBuilder.DropTable(
                name: "AnalysisCategories");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DosageForms");
        }
    }
}
