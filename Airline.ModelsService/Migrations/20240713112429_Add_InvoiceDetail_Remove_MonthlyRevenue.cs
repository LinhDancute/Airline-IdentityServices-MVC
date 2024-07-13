using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    /// <inheritdoc />
    public partial class Add_InvoiceDetail_Remove_MonthlyRevenue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_MonthlyRevenues_MonthlyRevenueId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "AnnualRevenues");

            migrationBuilder.DropTable(
                name: "MonthlyRevenues");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_MonthlyRevenueId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CMND",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "MonthlyRevenueId",
                table: "Invoices",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "InvoiceDate",
                table: "Invoices",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Itinerary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_TicketId",
                table: "InvoiceDetails",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Invoices",
                newName: "MonthlyRevenueId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Invoices",
                newName: "InvoiceDate");

            migrationBuilder.AddColumn<string>(
                name: "CMND",
                table: "Invoices",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StaffId",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AnnualRevenues",
                columns: table => new
                {
                    AnnualRevenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TicketByYear = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualRevenues", x => x.AnnualRevenueId);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyRevenues",
                columns: table => new
                {
                    MonthlyRevenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnualRevenueId = table.Column<int>(type: "int", nullable: true),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TicketByMonth = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyRevenues", x => x.MonthlyRevenueId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MonthlyRevenueId",
                table: "Invoices",
                column: "MonthlyRevenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_MonthlyRevenues_MonthlyRevenueId",
                table: "Invoices",
                column: "MonthlyRevenueId",
                principalTable: "MonthlyRevenues",
                principalColumn: "MonthlyRevenueId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
