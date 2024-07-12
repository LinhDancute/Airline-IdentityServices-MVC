using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Relationship_BoardingPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardingPasses_AspNetUsers_PassengerId",
                table: "BoardingPasses");

            migrationBuilder.DropIndex(
                name: "IX_BoardingPasses_PassengerId",
                table: "BoardingPasses");

            migrationBuilder.AlterColumn<string>(
                name: "PassengerId",
                table: "BoardingPasses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PassengerId",
                table: "BoardingPasses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_PassengerId",
                table: "BoardingPasses",
                column: "PassengerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardingPasses_AspNetUsers_PassengerId",
                table: "BoardingPasses",
                column: "PassengerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
