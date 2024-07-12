using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Rela_BoardingPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardingPasses_Baggages_BaggageId",
                table: "BoardingPasses");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardingPasses_Flights_FlightId",
                table: "BoardingPasses");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardingPasses_Meals_MealId",
                table: "BoardingPasses");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardingPasses_TicketClasses_TicketId",
                table: "BoardingPasses");

            migrationBuilder.DropIndex(
                name: "IX_BoardingPasses_BaggageId",
                table: "BoardingPasses");

            migrationBuilder.DropIndex(
                name: "IX_BoardingPasses_FlightId",
                table: "BoardingPasses");

            migrationBuilder.DropIndex(
                name: "IX_BoardingPasses_MealId",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "BaggageId",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "PassengerId",
                table: "BoardingPasses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaggageId",
                table: "BoardingPasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "BoardingPasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "BoardingPasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "BoardingPasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PassengerId",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_BaggageId",
                table: "BoardingPasses",
                column: "BaggageId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_FlightId",
                table: "BoardingPasses",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_MealId",
                table: "BoardingPasses",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardingPasses_Baggages_BaggageId",
                table: "BoardingPasses",
                column: "BaggageId",
                principalTable: "Baggages",
                principalColumn: "BaggageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardingPasses_Flights_FlightId",
                table: "BoardingPasses",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardingPasses_Meals_MealId",
                table: "BoardingPasses",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardingPasses_TicketClasses_TicketId",
                table: "BoardingPasses",
                column: "TicketId",
                principalTable: "TicketClasses",
                principalColumn: "TicketId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
