using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.ModelsService.Migrations
{
    public partial class Change_Name_TicketClassId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * 
                    FROM sys.foreign_keys 
                    WHERE name = 'FK_Tickets_TicketClasses_TicketId'
                )
                BEGIN
                    ALTER TABLE Tickets DROP CONSTRAINT FK_Tickets_TicketClasses_TicketId;
                END
            ");

            // Drop index if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * 
                    FROM sys.indexes 
                    WHERE name = 'IX_Tickets_ClassId'
                )
                BEGIN
                    DROP INDEX IX_Tickets_ClassId ON Tickets;
                END
            ");

            // Drop old column and primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketClasses",
                table: "TicketClasses");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketClasses");

            // Add new column with identity
            migrationBuilder.AddColumn<int>(
                name: "TicketClassId",
                table: "TicketClasses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Add new primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketClasses",
                table: "TicketClasses",
                column: "TicketClassId");

            // Recreate index
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClassId",
                table: "Tickets",
                column: "ClassId");

            // Recreate foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketClasses_ClassId",
                table: "Tickets",
                column: "ClassId",
                principalTable: "TicketClasses",
                principalColumn: "TicketClassId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * 
                    FROM sys.foreign_keys 
                    WHERE name = 'FK_Tickets_TicketClasses_ClassId'
                )
                BEGIN
                    ALTER TABLE Tickets DROP CONSTRAINT FK_Tickets_TicketClasses_ClassId;
                END
            ");

            // Drop index if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * 
                    FROM sys.indexes 
                    WHERE name = 'IX_Tickets_ClassId'
                )
                BEGIN
                    DROP INDEX IX_Tickets_ClassId ON Tickets;
                END
            ");

            // Drop new column and primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketClasses",
                table: "TicketClasses");

            migrationBuilder.DropColumn(
                name: "TicketClassId",
                table: "TicketClasses");

            // Add old column with identity
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "TicketClasses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Add old primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketClasses",
                table: "TicketClasses",
                column: "TicketId");

            // Recreate index
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketId",
                table: "Tickets",
                column: "TicketId");

            // Recreate foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketClasses_TicketId",
                table: "Tickets",
                column: "TicketId",
                principalTable: "TicketClasses",
                principalColumn: "TicketId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
