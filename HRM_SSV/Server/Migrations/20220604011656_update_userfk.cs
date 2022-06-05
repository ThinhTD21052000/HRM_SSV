using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class update_userfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaborContract_AspNetUsers_UserId",
                table: "LaborContract");

            migrationBuilder.DropForeignKey(
                name: "FK_MonthlySalary_AspNetUsers_UserId",
                table: "MonthlySalary");

            migrationBuilder.DropForeignKey(
                name: "FK_Timekeeping_AspNetUsers_UserId",
                table: "Timekeeping");

            migrationBuilder.DropForeignKey(
                name: "FK_Wage_AspNetUsers_UserId",
                table: "Wage");

            migrationBuilder.DropIndex(
                name: "IX_Wage_UserId",
                table: "Wage");

            migrationBuilder.DropIndex(
                name: "IX_Timekeeping_UserId",
                table: "Timekeeping");

            migrationBuilder.DropIndex(
                name: "IX_MonthlySalary_UserId",
                table: "MonthlySalary");

            migrationBuilder.DropIndex(
                name: "IX_LaborContract_UserId",
                table: "LaborContract");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Timekeeping",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MonthlySalary",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LaborContract",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Wage",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Timekeeping",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "MonthlySalary",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LaborContract",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_Wage_UserId",
                table: "Wage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Timekeeping_UserId",
                table: "Timekeeping",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlySalary_UserId",
                table: "MonthlySalary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LaborContract_UserId",
                table: "LaborContract",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaborContract_AspNetUsers_UserId",
                table: "LaborContract",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlySalary_AspNetUsers_UserId",
                table: "MonthlySalary",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Timekeeping_AspNetUsers_UserId",
                table: "Timekeeping",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wage_AspNetUsers_UserId",
                table: "Wage",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
