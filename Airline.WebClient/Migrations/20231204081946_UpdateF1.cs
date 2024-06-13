using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservationVietjet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateF1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlightSector",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightSector",
                table: "Flights");
        }
    }
}
