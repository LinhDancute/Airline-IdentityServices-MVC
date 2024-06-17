using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservationVietjet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ICAOcode",
                table: "Airline",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ICAOcode",
                table: "Airline");
        }
    }
}
