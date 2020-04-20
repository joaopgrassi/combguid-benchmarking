using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableWithCombGuid",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithCombGuid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableWithIdentity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithIdentity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableWithRegularGuid",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithRegularGuid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableWithRTCombGuid",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithRTCombGuid", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableWithCombGuid_Value",
                table: "TableWithCombGuid",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_TableWithIdentity_Value",
                table: "TableWithIdentity",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_TableWithRegularGuid_Value",
                table: "TableWithRegularGuid",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_TableWithRTCombGuid_Value",
                table: "TableWithRTCombGuid",
                column: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableWithCombGuid");

            migrationBuilder.DropTable(
                name: "TableWithIdentity");

            migrationBuilder.DropTable(
                name: "TableWithRegularGuid");

            migrationBuilder.DropTable(
                name: "TableWithRTCombGuid");
        }
    }
}
