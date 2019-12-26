using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp.Migrations
{
    public partial class DefaultCustomGuidInSql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableWithCustomGuidInSql",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(CONVERT([uniqueidentifier],CONVERT([binary](10),newid(),(0))+CONVERT([binary](6),getutcdate(),(0)),(0)))"),
                    AnotherId = table.Column<Guid>(nullable: false, defaultValueSql: "(CONVERT([uniqueidentifier],CONVERT([binary](10),newid(),(0))+CONVERT([binary](6),getutcdate(),(0)),(0)))"),
                    Value = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableWithCustomGuidInSql", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableWithCustomGuidInSql_AnotherId_Value",
                table: "TableWithCustomGuidInSql",
                columns: new[] { "AnotherId", "Value" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableWithCustomGuidInSql");
        }
    }
}
