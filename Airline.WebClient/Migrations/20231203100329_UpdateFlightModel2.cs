using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservationVietjet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFlightModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aircraft",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aircraft",
                table: "Flights");
        }
    }
}
