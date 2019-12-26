using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp.Migrations
{
    public partial class VbCombGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableWithVbCombGuid",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnotherId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithVbCombGuid", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableWithVbCombGuid_AnotherId_Value",
                table: "TableWithVbCombGuid",
                columns: new[] { "AnotherId", "Value" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableWithVbCombGuid");
        }
    }
}
