using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableWithExtendedUuidCreateSequential",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnotherId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithExtendedUuidCreateSequential", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableWithNewSequentialIdAsDefault",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnotherId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithNewSequentialIdAsDefault", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableWithRegularGuid",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnotherId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithRegularGuid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableWithSpanCustomGuidComb",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnotherId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithSpanCustomGuidComb", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableWithExtendedUuidCreateSequential_AnotherId_Value",
                table: "TableWithExtendedUuidCreateSequential",
                columns: new[] { "AnotherId", "Value" });

            migrationBuilder.CreateIndex(
                name: "IX_TableWithNewSequentialIdAsDefault_AnotherId_Value",
                table: "TableWithNewSequentialIdAsDefault",
                columns: new[] { "AnotherId", "Value" });

            migrationBuilder.CreateIndex(
                name: "IX_TableWithRegularGuid_AnotherId_Value",
                table: "TableWithRegularGuid",
                columns: new[] { "AnotherId", "Value" });

            migrationBuilder.CreateIndex(
                name: "IX_TableWithSpanCustomGuidComb_AnotherId_Value",
                table: "TableWithSpanCustomGuidComb",
                columns: new[] { "AnotherId", "Value" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableWithExtendedUuidCreateSequential");

            migrationBuilder.DropTable(
                name: "TableWithNewSequentialIdAsDefault");

            migrationBuilder.DropTable(
                name: "TableWithRegularGuid");

            migrationBuilder.DropTable(
                name: "TableWithSpanCustomGuidComb");
        }
    }
}
