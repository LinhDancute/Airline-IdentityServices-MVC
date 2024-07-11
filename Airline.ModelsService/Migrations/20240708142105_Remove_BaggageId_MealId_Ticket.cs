using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    /// <inheritdoc />
    public partial class Remove_BaggageId_MealId_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Baggages_BaggageId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Meals_MealId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_BaggageId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_MealId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "BaggageId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Tickets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BoardingTime",
                table: "BoardingPasses",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaggageId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BoardingTime",
                table: "BoardingPasses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BaggageId",
                table: "Tickets",
                column: "BaggageId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MealId",
                table: "Tickets",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Baggages_BaggageId",
                table: "Tickets",
                column: "BaggageId",
                principalTable: "Baggages",
                principalColumn: "BaggageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Meals_MealId",
                table: "Tickets",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
