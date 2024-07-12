using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Column_Ticket_BoardingPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "BaggageType",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "ETicket",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "FlightNumber",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "Itinerary",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "PNR",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "PassengerName",
                table: "BoardingPasses");

            migrationBuilder.DropColumn(
                name: "SSR",
                table: "BoardingPasses");

            migrationBuilder.AddColumn<string>(
                name: "PassengerPhoneNumber",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BoardingTime",
                table: "BoardingPasses",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassengerPhoneNumber",
                table: "Tickets");

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "BoardingTime",
                table: "BoardingPasses",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "BaggageType",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BoardingPasses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DepartureTime",
                table: "BoardingPasses",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "ETicket",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FlightNumber",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Itinerary",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PNR",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassengerName",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SSR",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
