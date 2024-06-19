using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservationVietjet.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightTime",
                table: "Flights");

            migrationBuilder.AddColumn<float>(
                name: "FlightTime",
                table: "FlightRoutes",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightTime",
                table: "FlightRoutes");

            migrationBuilder.AddColumn<float>(
                name: "FlightTime",
                table: "Flights",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
