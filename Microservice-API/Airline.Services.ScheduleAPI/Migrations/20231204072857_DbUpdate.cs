using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservationVietjet.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightTime",
                table: "FlightRoutes");

            migrationBuilder.RenameColumn(
                name: "DeparturetTime",
                table: "Flights",
                newName: "DepartureTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "Flights",
                newName: "DeparturetTime");

            migrationBuilder.AddColumn<float>(
                name: "FlightTime",
                table: "FlightRoutes",
                type: "real",
                nullable: true);
        }
    }
}
