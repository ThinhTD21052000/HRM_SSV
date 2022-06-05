using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class create_monthtimekeepingtb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MTKId",
                table: "Timekeeping",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MonthTimekeeping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthTimekeeping", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timekeeping_MTKId",
                table: "Timekeeping",
                column: "MTKId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timekeeping_MonthTimekeeping_MTKId",
                table: "Timekeeping",
                column: "MTKId",
                principalTable: "MonthTimekeeping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timekeeping_MonthTimekeeping_MTKId",
                table: "Timekeeping");

            migrationBuilder.DropTable(
                name: "MonthTimekeeping");

            migrationBuilder.DropIndex(
                name: "IX_Timekeeping_MTKId",
                table: "Timekeeping");

            migrationBuilder.DropColumn(
                name: "MTKId",
                table: "Timekeeping");
        }
    }
}
