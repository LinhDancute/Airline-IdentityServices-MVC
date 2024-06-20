using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservationVietjet.Migrations
{
    /// <inheritdoc />
    public partial class Model_BoardingPass_AddColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardingPasses_AspNetUsers_AppUserId",
                table: "BoardingPasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AppUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_UnitPrices_UnitPricePriceId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AppUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UnitPricePriceId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_BoardingPasses_AppUserId",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UnitPricePriceId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "BoardingPasses");

            migrationBuilder.RenameColumn(
                name: "CMND",
                table: "BoardingPasses",
                newName: "Gate");

            migrationBuilder.RenameColumn(
                name: "BookingDate",
                table: "BoardingPasses",
                newName: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "Seat",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "BoardingTime",
                table: "BoardingPasses",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DepartureTime",
                table: "BoardingPasses",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "FlightNumber",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassengerName",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardingTime",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "FlightNumber",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "PassengerName",
                table: "BoardingPasses");

            migrationBuilder.RenameColumn(
                name: "Gate",
                table: "BoardingPasses",
                newName: "CMND");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "BoardingPasses",
                newName: "BookingDate");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitPricePriceId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Seat",
                table: "BoardingPasses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "BoardingPasses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AppUserId",
                table: "Tickets",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UnitPricePriceId",
                table: "Tickets",
                column: "UnitPricePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_AppUserId",
                table: "BoardingPasses",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardingPasses_AspNetUsers_AppUserId",
                table: "BoardingPasses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AppUserId",
                table: "Tickets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_UnitPrices_UnitPricePriceId",
                table: "Tickets",
                column: "UnitPricePriceId",
                principalTable: "UnitPrices",
                principalColumn: "PriceId");
        }
    }
}
