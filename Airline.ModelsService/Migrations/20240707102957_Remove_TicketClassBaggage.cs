using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    /// <inheritdoc />
    public partial class Remove_TicketClassBaggage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketClass_Baggages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketClass_Baggages",
                columns: table => new
                {
                    TicketClassID = table.Column<int>(type: "int", nullable: false),
                    BaggageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketClass_Baggages", x => new { x.TicketClassID, x.BaggageID });
                    table.ForeignKey(
                        name: "FK_TicketClass_Baggages_Baggages_BaggageID",
                        column: x => x.BaggageID,
                        principalTable: "Baggages",
                        principalColumn: "BaggageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketClass_Baggages_TicketClasses_TicketClassID",
                        column: x => x.TicketClassID,
                        principalTable: "TicketClasses",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketClass_Baggages_BaggageID",
                table: "TicketClass_Baggages",
                column: "BaggageID");
        }
    }
}
