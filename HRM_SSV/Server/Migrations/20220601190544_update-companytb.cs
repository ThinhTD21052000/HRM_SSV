using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class updatecompanytb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                table: "Company",
                type: "VARCHAR(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                table: "Company",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(12)",
                oldMaxLength: 12);
        }
    }
}
