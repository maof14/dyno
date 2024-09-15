using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedByUser",
                table: "MeasurementResult",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeChanged",
                table: "MeasurementResult",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ChangedByUser",
                table: "Measurement",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeChanged",
                table: "Measurement",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    DateTimeChanged = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ChangedByUser = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ChangedByUser",
                table: "MeasurementResult");

            migrationBuilder.DropColumn(
                name: "DateTimeChanged",
                table: "MeasurementResult");

            migrationBuilder.DropColumn(
                name: "ChangedByUser",
                table: "Measurement");

            migrationBuilder.DropColumn(
                name: "DateTimeChanged",
                table: "Measurement");
        }
    }
}
