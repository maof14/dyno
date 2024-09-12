using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementResult_Measurement_Id",
                table: "MeasurementResult");

            migrationBuilder.AddColumn<Guid>(
                name: "MeasurementId",
                table: "MeasurementResult",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementResult_MeasurementId",
                table: "MeasurementResult",
                column: "MeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementResult_Measurement_MeasurementId",
                table: "MeasurementResult",
                column: "MeasurementId",
                principalTable: "Measurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementResult_Measurement_MeasurementId",
                table: "MeasurementResult");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementResult_MeasurementId",
                table: "MeasurementResult");

            migrationBuilder.DropColumn(
                name: "MeasurementId",
                table: "MeasurementResult");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementResult_Measurement_Id",
                table: "MeasurementResult",
                column: "Id",
                principalTable: "Measurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
