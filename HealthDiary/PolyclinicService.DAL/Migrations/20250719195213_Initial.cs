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
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор врача")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Seniority = table.Column<byte>(type: "smallint", nullable: false, comment: "Стаж врача"),
                    QualificationType = table.Column<byte>(type: "smallint", nullable: false, comment: "Квалификация врача"),
                    AcademyDegree = table.Column<byte>(type: "smallint", nullable: true, comment: "Научная степень врача"),
                    IsConfirmedEducation = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак, что у врача подтвержден документ об образовании"),
                    IsConfirmedQualification = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак, что у врача подтвержден документ о квалификации"),
                    SpecializationType = table.Column<short>(type: "smallint", nullable: false, comment: "Специализация врача")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                },
                comment: "Врачи поликлиник");

            migrationBuilder.CreateTable(
                name: "Polyclinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор поликлиники")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "Наименование поликлиники"),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "Адрес"),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false, comment: "Номер телефона"),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Адрес электронной почты"),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "Ссылка на сайт")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polyclinics", x => x.Id);
                },
                comment: "Поликлиники");

            migrationBuilder.CreateTable(
                name: "AppointmentSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор слота приёма в графике")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор врача"),
                    PolyclinicId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор поликлинники"),
                    UserId = table.Column<int>(type: "integer", nullable: true, comment: "Идентификатор пользователя (пациента)"),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата приёма"),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false, comment: "Продолжительность приёма"),
                    Status = table.Column<byte>(type: "smallint", nullable: false, comment: "Статус приёма в графике")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentSlots_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentSlots_Polyclinics_PolyclinicId",
                        column: x => x.PolyclinicId,
                        principalTable: "Polyclinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Слоты приёма к врачу в графике поликлиники");

            migrationBuilder.CreateTable(
                name: "PolyclinicDoctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    PolyclinicId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolyclinicDoctors", x => new { x.DoctorId, x.PolyclinicId });
                    table.ForeignKey(
                        name: "FK_PolyclinicDoctors_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolyclinicDoctors_Polyclinics_PolyclinicId",
                        column: x => x.PolyclinicId,
                        principalTable: "Polyclinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор результата приёма")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ReportContent = table.Column<string>(type: "text", nullable: false, comment: "Содержание отчёта по приёму пациента"),
                    AppointmentSlotId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор слота на приём к врачу из графика"),
                    ReportTemplateId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор шаблона отчёта")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentResults_AppointmentSlots_AppointmentSlotId",
                        column: x => x.AppointmentSlotId,
                        principalTable: "AppointmentSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Результаты приёмов пациентов");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentResults_AppointmentSlotId",
                table: "AppointmentResults",
                column: "AppointmentSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSlots_DoctorId",
                table: "AppointmentSlots",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSlots_PolyclinicId",
                table: "AppointmentSlots",
                column: "PolyclinicId");

            migrationBuilder.CreateIndex(
                name: "IX_PolyclinicDoctors_PolyclinicId",
                table: "PolyclinicDoctors",
                column: "PolyclinicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentResults");

            migrationBuilder.DropTable(
                name: "PolyclinicDoctors");

            migrationBuilder.DropTable(
                name: "AppointmentSlots");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Polyclinics");
        }
    }
}
