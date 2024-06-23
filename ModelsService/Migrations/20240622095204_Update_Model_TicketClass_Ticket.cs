using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

<<<<<<< HEAD:ModelsService/Migrations/20240622095204_Update_Model_TicketClass_Ticket.cs
namespace Airline.ModelsService.Migrations
=======
namespace Airline.WebClient.Migrations
>>>>>>> 015933b5a74e5f2f345a2bfbb51871285fa0aac9:Airline.WebClient/Migrations/20240622095204_Update_Model_TicketClass_Ticket.cs
{
    /// <inheritdoc />
    public partial class Update_Model_TicketClass_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TicketName",
                table: "TicketClasses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "FareClass",
                table: "TicketClasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "FareClass",
                table: "TicketClasses");

            migrationBuilder.AlterColumn<string>(
                name: "TicketName",
                table: "TicketClasses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
