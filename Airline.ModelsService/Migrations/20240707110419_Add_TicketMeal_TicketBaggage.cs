using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    /// <inheritdoc />
    public partial class Add_TicketMeal_TicketBaggage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ticket_Baggages",
                columns: table => new
                {
                    TicketID = table.Column<int>(type: "int", nullable: false),
                    BaggageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket_Baggages", x => new { x.TicketID, x.BaggageID });
                    table.ForeignKey(
                        name: "FK_Ticket_Baggages_Baggages_BaggageID",
                        column: x => x.BaggageID,
                        principalTable: "Baggages",
                        principalColumn: "BaggageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Baggages_Tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket_Meals",
                columns: table => new
                {
                    TicketID = table.Column<int>(type: "int", nullable: false),
                    MealID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket_Meals", x => new { x.TicketID, x.MealID });
                    table.ForeignKey(
                        name: "FK_Ticket_Meals_Meals_MealID",
                        column: x => x.MealID,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Meals_Tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_Baggages_BaggageID",
                table: "Ticket_Baggages",
                column: "BaggageID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_Meals_MealID",
                table: "Ticket_Meals",
                column: "MealID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket_Baggages");

            migrationBuilder.DropTable(
                name: "Ticket_Meals");
        }
    }
}
