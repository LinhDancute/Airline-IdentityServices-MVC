using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    /// <inheritdoc />
    public partial class Drop_ForeignKey_BoardingPass_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardingPasses_Tickets_TicketId",
                table: "BoardingPasses");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardingPasses_Tickets_TicketId",
                table: "BoardingPasses",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "TicketId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardingPasses_Tickets_TicketId",
                table: "BoardingPasses");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardingPasses_Tickets_TicketId",
                table: "BoardingPasses",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "TicketId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
