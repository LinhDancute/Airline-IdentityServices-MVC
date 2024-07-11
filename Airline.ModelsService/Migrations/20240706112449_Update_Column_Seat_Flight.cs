using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    /// <inheritdoc />
    public partial class Update_Column_Seat_Flight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeluxeSeat",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "SkyBossSeat",
                table: "Flights",
                newName: "PremiumEconomySeat");

            migrationBuilder.RenameColumn(
                name: "SkyBossBusinessSeat",
                table: "Flights",
                newName: "EconomySeat");

            migrationBuilder.RenameColumn(
                name: "EcoSeat",
                table: "Flights",
                newName: "BusinessSeat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PremiumEconomySeat",
                table: "Flights",
                newName: "SkyBossSeat");

            migrationBuilder.RenameColumn(
                name: "EconomySeat",
                table: "Flights",
                newName: "SkyBossBusinessSeat");

            migrationBuilder.RenameColumn(
                name: "BusinessSeat",
                table: "Flights",
                newName: "EcoSeat");

            migrationBuilder.AddColumn<int>(
                name: "DeluxeSeat",
                table: "Flights",
                type: "int",
                nullable: true);
        }
    }
}
