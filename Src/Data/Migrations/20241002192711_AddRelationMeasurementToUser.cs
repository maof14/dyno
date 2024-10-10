using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationMeasurementToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Measurement",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Measurement_AppUserId",
                table: "Measurement",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement_AppUsers_AppUserId",
                table: "Measurement",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurement_AppUsers_AppUserId",
                table: "Measurement");

            migrationBuilder.DropIndex(
                name: "IX_Measurement_AppUserId",
                table: "Measurement");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Measurement");
        }
    }
}
