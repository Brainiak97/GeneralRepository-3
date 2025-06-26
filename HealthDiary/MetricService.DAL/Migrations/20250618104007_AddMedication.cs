using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetricService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMedication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intakes");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Regimens");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "DosageForms");
        }
    }
}
