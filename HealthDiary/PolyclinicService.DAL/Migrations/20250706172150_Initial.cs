using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PolyclinicService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "polyclinics");

            migrationBuilder.CreateTable(
                name: "doctors",
                schema: "polyclinics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор врача")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    seniority = table.Column<short>(type: "smallint", nullable: false, comment: "Стаж врача"),
                    qualification_type = table.Column<short>(type: "smallint", nullable: false, comment: "Квалификация врача"),
                    academy_degree = table.Column<short>(type: "smallint", nullable: true, comment: "Научная степень врача"),
                    is_confirmed_education = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак, что у врача подтвержден документ об образовании"),
                    is_confirmed_qualification = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак, что у врача подтвержден документ о квалификации"),
                    specialization_type = table.Column<short>(type: "smallint", nullable: false, comment: "Специализация врача")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.id);
                },
                comment: "Врачи поликлиник");

            migrationBuilder.CreateTable(
                name: "polyclinics",
                schema: "polyclinics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор поликлиники")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "Наименование поликлиники"),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "Адрес"),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false, comment: "Номер телефона"),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Адрес электронной почты"),
                    url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "Ссылка на сайт")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_polyclinics", x => x.id);
                },
                comment: "Поликлиники");

            migrationBuilder.CreateTable(
                name: "appointment_slots",
                schema: "polyclinics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор приёма в графике")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    doctor_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор врача"),
                    polyclinic_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор поликлиники"),
                    user_id = table.Column<int>(type: "integer", nullable: true, comment: "Идентификатор записанного пациента"),
                    date = table.Column<DateOnly>(type: "date", nullable: false, comment: "Дата приёма"),
                    start_time = table.Column<TimeSpan>(type: "interval", nullable: false, comment: "Время начала приёма"),
                    end_time = table.Column<TimeSpan>(type: "interval", nullable: false, comment: "Время окончания приёма"),
                    status = table.Column<short>(type: "smallint", nullable: false, comment: "Статус приёма в графике")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment_slots", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointment_slots_doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "polyclinics",
                        principalTable: "doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_slots_polyclinics_polyclinic_id",
                        column: x => x.polyclinic_id,
                        principalSchema: "polyclinics",
                        principalTable: "polyclinics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Данные о приёмах к врачу в графиках поликлиники");

            migrationBuilder.CreateTable(
                name: "polyclinic_doctors",
                schema: "polyclinics",
                columns: table => new
                {
                    doctor_id = table.Column<int>(type: "integer", nullable: false),
                    polyclinic_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_polyclinic_doctors", x => new { x.doctor_id, x.polyclinic_id });
                    table.ForeignKey(
                        name: "FK_polyclinic_doctors_doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "polyclinics",
                        principalTable: "doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_polyclinic_doctors_polyclinics_polyclinic_id",
                        column: x => x.polyclinic_id,
                        principalSchema: "polyclinics",
                        principalTable: "polyclinics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointment_results",
                schema: "polyclinics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор результата приёма к врачу")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    report_content = table.Column<byte[]>(type: "bytea", nullable: false, comment: "Содержание отчёта приёма пациента"),
                    appointment_slot_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор слота на приём к врачу из графика"),
                    report_template_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор шаблона отчёта")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment_results", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointment_results_appointment_slots_appointment_slot_id",
                        column: x => x.appointment_slot_id,
                        principalSchema: "polyclinics",
                        principalTable: "appointment_slots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Результаты приёмов к врачам");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_results_appointment_slot_id",
                schema: "polyclinics",
                table: "appointment_results",
                column: "appointment_slot_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_appointment_slots_doctor_id",
                schema: "polyclinics",
                table: "appointment_slots",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_slots_polyclinic_id",
                schema: "polyclinics",
                table: "appointment_slots",
                column: "polyclinic_id");

            migrationBuilder.CreateIndex(
                name: "IX_polyclinic_doctors_polyclinic_id",
                schema: "polyclinics",
                table: "polyclinic_doctors",
                column: "polyclinic_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment_results",
                schema: "polyclinics");

            migrationBuilder.DropTable(
                name: "polyclinic_doctors",
                schema: "polyclinics");

            migrationBuilder.DropTable(
                name: "appointment_slots",
                schema: "polyclinics");

            migrationBuilder.DropTable(
                name: "doctors",
                schema: "polyclinics");

            migrationBuilder.DropTable(
                name: "polyclinics",
                schema: "polyclinics");
        }
    }
}
