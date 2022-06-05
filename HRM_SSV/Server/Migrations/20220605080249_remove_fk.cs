using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class remove_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timekeeping_MonthTimekeeping_MTKId",
                table: "Timekeeping");

            migrationBuilder.DropIndex(
                name: "IX_Timekeeping_MTKId",
                table: "Timekeeping");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
